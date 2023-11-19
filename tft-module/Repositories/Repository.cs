// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : Repository.cs
//           Basic methods for regualr use of the MongoDB PlayerAccpunt Collection

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using tft_module.Models;

namespace tft_module.Repositories;

public abstract class Repository
{
    protected readonly IMongoDatabase MongoDatabase;

    protected Repository(IOptions<TftDatabaseSettings> tftDatabaseSettings)
    {
        var mongoClient = new MongoClient(tftDatabaseSettings.Value.ConnectionString);
        MongoDatabase = mongoClient.GetDatabase(tftDatabaseSettings.Value.DatabaseName);
    }
}