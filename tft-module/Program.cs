// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : Program.cs
//           Constants needed by the whole application

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using tft_module.Models;
using tft_module.Repositories;
using tft_module.Repositories.Impl;
using tft_module.Services;
using tft_module.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver()
};

// Add Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection dependencies
builder.Services.AddTransient<IMatchesService, MatchesService>();
builder.Services.AddTransient<ISummonerService, SummonerService>();
builder.Services.AddTransient<IMatchRepository, MatchRepository>();
builder.Services.AddTransient<ISummonerRepository, SummonerRepository>();
builder.Services.AddTransient<ITftService, TftService>();



builder.Services.Configure<TftDatabaseSettings>(
    builder.Configuration.GetSection("TftDatabase"));

builder.Services.Configure<TftDatabaseSettings>(settings =>
    settings.ConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING") ??
                                throw new MissingFieldException(
                                    "Missing Environment Variable for mongoDB connection string"));



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