// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : SummonerRepository.cs
//           Basic methods for regular use of the MongoDB PlayerAccount Collection

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using tft_module.Models;
using tft_module.Models.Response;

namespace tft_module.Repositories.Impl;

public class SummonerRepository : Repository, ISummonerRepository
{
    private readonly IMongoCollection<SummonerResponse> _playerAccountCollection;

    public SummonerRepository(IOptions<TftDatabaseSettings> tftDatabaseSettings) : base(tftDatabaseSettings)
    {
        _playerAccountCollection =
            MongoDatabase.GetCollection<SummonerResponse>(tftDatabaseSettings.Value.SummonerCollectionName);
    }

    /// <summary>
    /// Get a Summoner from its name
    /// </summary>
    /// <param name="name">A <see cref="System.String"/> who contains the name of the summoner.</param>
    /// <returns>An instance of SummonerResponse or null if it is not in the database.</returns>
    public async Task<SummonerResponse?> GetSummonerByName(string name)
    {
        return await _playerAccountCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get a Summoner from its puuid
    /// </summary>
    /// <param name="puuid">A <see cref="System.String"/> who contains the puuid of the summoner.</param>
    /// <returns>An instance of SummonerResponse or null if it is not in the database.</returns>
    public async Task<SummonerResponse> GetSummonerByPuuid(string puuid)
    {
        return await _playerAccountCollection.Find(x => x.Puuid == puuid).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Add a Summoner to the database
    /// </summary>
    /// <param name="summonerResponse">An instance of <see cref="SummonerResponse"/> </param>
    public async Task Create(SummonerResponse summonerResponse)
    {
        await _playerAccountCollection.InsertOneAsync(summonerResponse);
    }
}
