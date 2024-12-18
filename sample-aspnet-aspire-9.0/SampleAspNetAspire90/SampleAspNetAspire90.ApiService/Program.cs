using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using SampleAspNetAspire90.ApiService.DataContext;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add Redis distributed cache.
builder.AddRedisDistributedCache("cache");

// Add Entity Framework for Sql Server support
builder.Services.AddDbContext<SampleAspNetAspire90.ApiService.DataContext.AspireDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AspireDbContext"));
});

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Initialize DB context 
app.CreateDbIfNotExists();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/productlist", async (SampleAspNetAspire90.ApiService.DataContext.AspireDbContext aspireDb) => 
{
    List<Product> products = new();
    products = await aspireDb.Product.ToListAsync();
    return products;
});

app.MapGet("/samples", async (IDistributedCache cache) =>
{
    var data = await cache.GetStringAsync("SampleKey");

    if (data is null)
    {
        data = Guid.NewGuid().ToString();
        await cache.SetStringAsync("SampleKey", data);
    }
    return data;
});

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
