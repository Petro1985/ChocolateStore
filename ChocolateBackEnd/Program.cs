using System.Reflection;
using ChocolateBackEnd;
using ChocolateBackEnd.Auth;
using ChocolateData;
using ChocolateData.Repositories;
using ChocolateDomain;
using ChocolateDomain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataBase(builder.Configuration.GetConnectionString());

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 3;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


builder.Services.AddScoped<IDbRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IDbRepository<Photo>, PhotoRepository>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<PhotoService>();

builder.Services.ConfigureApplicationCookie(conf =>
{
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
    AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();
    
    authorizationPolicyBuilder.RequireClaim("Admin");
    var authorizationPolicy = authorizationPolicyBuilder.Build();
    
    options.AddPolicy(Policies.Admin, authorizationPolicy);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();