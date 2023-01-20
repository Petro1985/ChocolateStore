using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ChocolateUI;
using ChocolateUI.Services;
using ChocolateUI.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiAddress = "https://localhost:7213";

// Http client registration
builder.Services
    .AddTransient<CookieHandler>()
    .AddScoped(sp => sp
        .GetRequiredService<IHttpClientFactory>()
        .CreateClient("API"))
    .AddHttpClient("API", client => client.BaseAddress = new Uri(apiAddress)).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddSingleton<IUserProfile, UserProfile>();
builder.Services.AddLogging();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();

await builder.Build().RunAsync();