using System.Reflection;
using ChocolateBackEnd;
using ChocolateData;
using ChocolateData.Repositories;
using ChocolateDomain;
using ChocolateDomain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
builder.Services.AddDataBase(builder.Configuration.GetConnectionString());
builder.Services.AddScoped<IDbRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IDbRepository<Photo>, PhotoRepository>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<PhotoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();