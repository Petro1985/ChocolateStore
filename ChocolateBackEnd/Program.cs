using System.Collections.Immutable;
using ChocolateBackEnd.APIStruct.Mapper;
using ChocolateBackEnd.Auth;
using ChocolateBackEnd.Options;
using ChocolateData;
using ChocolateData.Repositories;
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

builder.Services.AddDataBase(builder.Configuration.GetConnectionString());

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

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 3;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


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

builder.Services.AddAuthorization(options =>
{
    var adminPolicyBuilder = new AuthorizationPolicyBuilder();
    adminPolicyBuilder.RequireClaim("Admin");
    var adminPolicy = adminPolicyBuilder.Build();
    options.AddPolicy(PoliciesConstants.Admin, adminPolicy);
    
    var clientPolicyBuilder = new AuthorizationPolicyBuilder();
    clientPolicyBuilder.RequireClaim("PhoneNumber");
    var userPolicy = clientPolicyBuilder.Build();

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