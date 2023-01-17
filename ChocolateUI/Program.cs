using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ChocolateUI;
using ChocolateUI.Services;
using ChocolateUI.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpMessageHandler = new HttpClientHandler();

var httpClient = new HttpClient(httpMessageHandler) {BaseAddress = new Uri("https://localhost:7213")};

builder.Services.AddScoped(_ => httpClient);
builder.Services.AddLogging();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();

await builder.Build().RunAsync();