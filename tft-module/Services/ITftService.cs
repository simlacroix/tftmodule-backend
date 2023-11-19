// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : ITftService.cs
//           Interface for TftService

using System.Threading.Tasks;
using tft_module.Models.Response;

namespace tft_module.Services;

public interface ITftService
{
    /// <summary>
    /// Get a summoner by its name
    /// </summary>
    /// <param name="name"></param>
    /// <returns>A<see cref="SummonerResponse"/> if summoner exists.</returns>
    public Task<SummonerResponse> GetSummonerByName(string name);

    /// <summary>
    /// Check if Summoner exists.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Returns the name or throws an exception if not existant.</returns>
    /// <exception cref="UserNotFoundException"></exception>
    public Task<string> CheckIfSummonerExists(string name);

    /// <summary>
    /// Get a number of matches for a summoner
    /// </summary>
    /// <param name="name"></param>
    /// <param name="nbMatchesToFetch"></param>
    /// <returns><see cref="MatchesFromQueueAndTagResponse"/></returns>
    public Task<MatchesFromQueueAndTagResponse> GetMatchesForSummoner(string name, int nbMatchesToFetch = 20);
}
