// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : ISummonerService.cs
//           Interface for SummonerService

using tft_module.Models.Response;

namespace tft_module.Services;

public interface ISummonerService
{
    /// <summary>
    /// Get a summoner by its name
    /// </summary>
    /// <param name="name"></param>
    /// <returns>A<see cref="SummonerResponse"/> if summoner exists.</returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ArgumentException"></exception>
    public Task<SummonerResponse> GetSummonerByName(string name);

    /// <summary>
    /// Get a summoner by its puuid
    /// </summary>
    /// <param name="puuid"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ArgumentException"></exception>
    public Task<SummonerResponse> GetSummonerByPuuid(string puuid);
}
