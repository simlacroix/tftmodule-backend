// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : ISummonerRepository.cs
//           Interface for ISummonerRepository

using tft_module.Models.Response;

namespace tft_module.Repositories;

public interface ISummonerRepository
{
    /// <summary>
    /// Get a Summoner from its name
    /// </summary>
    /// <param name="name">A <see cref="System.String"/> who contains the name of the summoner.</param>
    /// <returns>An instance of SummonerResponse or null if it is not in the database.</returns>
    public Task<SummonerResponse?> GetSummonerByName(string name);

    /// <summary>
    /// Get a Summoner from its puuid
    /// </summary>
    /// <param name="puuid">A <see cref="System.String"/> who contains the puuid of the summoner.</param>
    /// <returns>An instance of SummonerResponse or null if it is not in the database.</returns>
    public Task<SummonerResponse> GetSummonerByPuuid(string puuid);

    /// <summary>
    /// Add a Summoner to the database
    /// </summary>
    /// <param name="summonerResponse">An instance of <see cref="SummonerResponse"/> </param>
    public Task Create(SummonerResponse summonerResponse);
}
