using System.Reflection;
using ChocolateBackEnd;
using ChocolateBackEnd.APIStruct.Mapper;
using ChocolateBackEnd.Auth;
using ChocolateData;
using ChocolateData.Repositories;
using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
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

builder.Services.AddCors(x =>
{
    x.AddPolicy("AnyOrigin", op =>
    {
        op.WithOrigins("http://localhost:5213", "https://localhost:7213", "https://localhost:7028");
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

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDbRepository<PhotoEntity>, PhotoRepository>();
builder.Services.AddScoped<IDbRepository<CategoryEntity>, CategoryRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.ConfigureApplicationCookie(conf =>
{
    conf.Cookie.SameSite = SameSiteMode.None;
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
    var authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

    authorizationPolicyBuilder.RequireClaim("Admin");
    var authorizationPolicy = authorizationPolicyBuilder.Build();

    options.AddPolicy(Policies.Admin, authorizationPolicy);
});

var app = builder.Build();

app.UseCors("AnyOrigin");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(op => op.EnablePersistAuthorization());
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();