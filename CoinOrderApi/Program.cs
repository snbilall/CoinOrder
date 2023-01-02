using CoinOrderApi;
using CoinOrderApi.Data;
using CoinOrderApi.Middlewares;
using CoinOrderApi.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppConfig>(options =>
{
    options.FirstDayOfMonthToOrder = int.Parse(builder.Configuration["FirstDayOfMonthToOrder"] ?? "1");
    options.LastDayOfMonthToOrder = int.Parse(builder.Configuration["LastDayOfMonthToOrder"] ?? "28");
    options.MinOrderablePrice = decimal.Parse(builder.Configuration["MinOrderablePrice"] ?? "100");
    options.MaxOrderablePrice = decimal.Parse(builder.Configuration["MaxOrderablePrice"] ?? "20000");
});

var server = builder.Configuration["DbServer"] ?? "localhost";
var port = builder.Configuration["DbPort"] ?? "1433"; // Default SQL Server port
var user = builder.Configuration["DbUser"] ?? "SA"; // Warning do not use the SA account
var password = builder.Configuration["Password"] ?? "Secret1234";
var database = builder.Configuration["Database"] ?? "OrderAppDb";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer($"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password};TrustServerCertificate=True")
);

builder.Services.AddProviders();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<AppDbContext>();
    if (context?.Database.IsRelational() == true)
    {
        context.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }
