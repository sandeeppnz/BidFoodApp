using BidFood.Api;
using BidFood.Data.Json.Repository;
using BidFood.Domain.Interfaces;
using BidFood.Domain.Models;
using JsonFlatFileDataStore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var settings = builder.Configuration.GetSection("DataSettings").Get<DataSettings>();
builder.Services.AddSingleton<IDataStore>(new DataStore(settings.FileName));
builder.Services.AddScoped(typeof(IDataProvider<Person>), typeof(PersonsRepository));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
