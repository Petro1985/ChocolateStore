using ChocolateData;
using ChocolateData.Repositories;
using Services;
using Services.Photo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("ChocolateDb");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new ApplicationException("Не задана строка подключения к ChocolateDb в файле конфигурации");
}

builder.Services.AddDataBase(connectionString);
builder.Services.AddControllers();
// регистрация сервисов
builder.Services.AddAppServices();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// Регистрация опций
builder.Services.AddOptions<PhotoServiceOptions>()
    .BindConfiguration(PhotoServiceOptions.Path);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();