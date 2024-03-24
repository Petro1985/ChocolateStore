using Asp.Versioning;
using ChocolateBackEnd.APIStruct.Mapper;
using ChocolateBackEnd.Auth;
using ChocolateBackEnd.Options;
using ChocolateData;
using ChocolateData.Repositories;
using ChocolateDomain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using Quartz;
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
    }).AddEntityFrameworkStores<OpenIddictDbContext>()
    .AddDefaultTokenProviders();
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

// Регистрация OpenIddict
builder.Services.AddDbContext<OpenIddictDbContext>(options =>
{
    // Configure the context to use sqlite.
    options.UseNpgsql("Server=localhost;Port=5444;Database=Chocolate_Identity;user id=postgres;password=123qwe!@#QWE");

    // Register the entity sets needed by OpenIddict.
    // Note: use the generic overload if you need
    // to replace the default OpenIddict entities.
    options.UseOpenIddict();
});

// // Register the Identity builder.Services.
// builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//     .AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddDefaultTokenProviders()
//     .AddDefaultUI();

// OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
// (like pruning orphaned authorizations/tokens from the database) at regular intervals.
builder.Services.AddQuartz(options =>
{
    options.UseMicrosoftDependencyInjectionJobFactory();
    options.UseSimpleTypeLoader();
    options.UseInMemoryStore();
});

// Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

builder.Services.AddOpenIddict()

    // Register the OpenIddict core components.
    .AddCore(options =>
    {
        // Configure OpenIddict to use the Entity Framework Core stores and models.
        // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
        options.UseEntityFrameworkCore()
            .UseDbContext<OpenIddictDbContext>();

        // Enable Quartz.NET integration.
        options.UseQuartz();
    })

    // Register the OpenIddict client components.
    .AddClient(options =>
    {
        // Note: this sample uses the code flow, but you can enable the other flows if necessary.
        options.AllowAuthorizationCodeFlow();

        // Register the signing and encryption credentials used to protect
        // sensitive data like the state tokens produced by OpenIddict.
        options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();

        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
        options.UseAspNetCore()
            .EnableStatusCodePagesIntegration()
            .EnableRedirectionEndpointPassthrough();

        // Register the System.Net.Http integration and use the identity of the current
        // assembly as a more specific user agent, which can be useful when dealing with
        // providers that use the user agent as a way to throttle requests (e.g Reddit).
        options.UseSystemNetHttp()
            .SetProductInformation(typeof(Program).Assembly);

        // Register the Web providers integrations.
        //
        // Note: to mitigate mix-up attacks, it's recommended to use a unique redirection endpoint
        // URI per provider, unless all the registered providers support returning a special "iss"
        // parameter containing their URL as part of authorization responses. For more information,
        // see https://datatracker.ietf.org/doc/html/draft-ietf-oauth-security-topics#section-4.4.
        options.UseWebProviders()
            .AddGitHub(options =>
            {
                options.SetClientId("c4ade52327b01ddacff3")
                    .SetClientSecret("da6bed851b75e317bf6b2cb67013679d9467c122")
                    .SetRedirectUri("callback/login/github");
            });
    })

    // Register the OpenIddict server components.
    .AddServer(options =>
    {
        // Enable the authorization, logout, token and userinfo endpoints.
        options.SetAuthorizationEndpointUris("connect/authorize")
            .SetLogoutEndpointUris("connect/logout")
            .SetTokenEndpointUris("connect/token")
            .SetUserinfoEndpointUris("connect/userinfo");

        // Mark the "email", "profile" and "roles" scopes as supported scopes.
        options.RegisterScopes(OpenIddictConstants.Scopes.Email, OpenIddictConstants.Scopes.Profile,
            OpenIddictConstants.Scopes.Roles);

        // Note: this sample only uses the authorization code flow but you can enable
        // the other flows if you need to support implicit, password or client credentials.
        options.AllowAuthorizationCodeFlow();

        // Register the signing and encryption credentials.
        options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();

        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
        options.UseAspNetCore()
            .EnableAuthorizationEndpointPassthrough()
            .EnableLogoutEndpointPassthrough()
            .EnableTokenEndpointPassthrough()
            .EnableUserinfoEndpointPassthrough()
            .EnableStatusCodePagesIntegration();
    })

    // Register the OpenIddict validation components.
    .AddValidation(options =>
    {
        // Import the configuration from the local OpenIddict server instance.
        options.UseLocalServer();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });

builder.Services.AddHostedService<Worker>();

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
// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// app.MapFallbackToFile("index.html");

app.Run();