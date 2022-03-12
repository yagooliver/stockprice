using System.ComponentModel;
using Microsoft.OpenApi.Models;
using StockPrice.Api;
using StockPrice.WebServices.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Stock Price"
    });
    foreach(var xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.AllDirectories))
    {
        options.IncludeXmlComments(xmlFile);
    }

});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
    
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapGet("/stock", async (string stock, IStockCallerService service) => await service.GetStock(stock));

// app.UseAuthorization();

// app.MapControllers();

app.Run();
