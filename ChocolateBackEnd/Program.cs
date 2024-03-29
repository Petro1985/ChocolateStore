using Asp.Versioning;
using ChocolateBackEnd.APIStruct.Mapper;
using ChocolateBackEnd.Auth;
using ChocolateBackEnd.Options;
using ChocolateData;
using ChocolateData.Repositories;
using ChocolateDomain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Services.Photo;
using Services.Product;
using Services.Profiler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfile<ServicesProfile>();
    opt.AddProfile<ProductMapperProfile>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString();
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new ApplicationException("Не задана строка подключения к БД в файле конфигурации");
}
builder.Services.AddDataBase(connectionString);

// Регистрация опций
builder.Services.AddOptions<PhotoServiceOptions>()
    .BindConfiguration(PhotoServiceOptions.Path);

builder.Services.AddCors(x =>
{
    x.AddPolicy("AnyOrigin", op =>
    {
        var corsOptions = builder.Configuration.GetSection("CORS").Get<CorsOptions>();
        if (corsOptions is null)
        {
            throw new Exception("Configuration error (CORS section is absent)");
        }
        
        op.WithOrigins(corsOptions.AllowedOrigin.Split(';'));

        Console.WriteLine($"CORS origin set to: {corsOptions.AllowedOrigin}");
        // op.AllowAnyOrigin();
        op.AllowAnyMethod();
        op.AllowAnyHeader();
        op.AllowCredentials();
    });
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 3;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsFactory>();


// Регистрация репозиториев
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// регистрация сервисов
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.ConfigureApplicationCookie(conf =>
{
    conf.Cookie.SameSite = SameSiteMode.Unspecified;
    conf.Cookie.SecurePolicy = CookieSecurePolicy.None;
    conf.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };

    conf.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
    x.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddAuthorization(options =>
{
    var adminPolicyBuilder = new AuthorizationPolicyBuilder();
    adminPolicyBuilder.RequireClaim(PoliciesConstants.AdminClaim);
    var adminPolicy = adminPolicyBuilder.Build();
    options.AddPolicy(PoliciesConstants.Admin, adminPolicy);
    
    var clientPolicyBuilder = new AuthorizationPolicyBuilder();
    clientPolicyBuilder.RequireClaim(PoliciesConstants.ClientClaim);
    var clientPolicy = clientPolicyBuilder.Build();
    options.AddPolicy(PoliciesConstants.Client, clientPolicy);
});

var app = builder.Build();

app.UseCors("AnyOrigin");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(op => op.EnablePersistAuthorization());
}

app.UseStaticFiles(new StaticFileOptions()
{
    ServeUnknownFileTypes = true
});
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// app.MapFallbackToFile("index.html");

app.Run();