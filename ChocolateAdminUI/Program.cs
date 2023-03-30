using System.Text.Json;
using ChocolateAdminUI;
using ChocolateAdminUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var urlsSection = builder.Configuration.GetSection("Urls");
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
    .AddHttpClient("API", client =>
    {
        client.BaseAddress = new Uri(serverApi);
    }).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddSingleton<IUserProfile, UserProfile>();
builder.Services.AddLogging();

builder.Services.AddScoped<CategoryState>();

builder.Services.AddScoped<IFetchService, FetchService>(x => new FetchService(
    x.GetRequiredService<HttpClient>(),
    x.GetRequiredService<ILogger<FetchService>>(),
    serverApi));
builder.Services.AddScoped<IUserService, UserService>();

await builder.Build().RunAsync();