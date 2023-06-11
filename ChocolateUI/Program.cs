using System.Net.Security;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ChocolateUI;
using ChocolateUI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var urlsSection = builder.Configuration
    .GetSection("ChocolateStoreSettings")
    .GetSection("Urls");
var serverApi = urlsSection["ServerApi"];

if (serverApi is null)
{
    throw new Exception("Ошибка загрузки файла конфигурации.");
}
Console.WriteLine($"ServerApi: {serverApi}");
// Http client registration
builder.Services
    .AddTransient<CookieHandler>()
    .AddScoped(sp => sp
        .GetRequiredService<IHttpClientFactory>()
        .CreateClient("API"))
    .AddHttpClient<IFetchService, FetchService>("API", client => client.BaseAddress = new Uri(serverApi))
    .AddHttpMessageHandler<CookieHandler>();
builder.Services.AddTransient<IFetchService, FetchService>();

builder.Services.AddSingleton<IUserProfile, UserProfile>();
builder.Services.AddLogging();

builder.Services.AddScoped<CategoryState>();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

await app.RunAsync();

