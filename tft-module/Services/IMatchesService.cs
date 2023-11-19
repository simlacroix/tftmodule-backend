// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : IMatchesService.cs
//           Interface for MatchesService

using tft_module.Models.Response;

namespace tft_module.Services;

public interface IMatchesService
{
    /// <summary>
    /// Get a number of latest match id for a summoner
    /// </summary>
    /// <param name="summonerPuuid"></param>
    /// <param name="nbMatchesToFetch"></param>
    /// <returns>A list of <see cref="System.String"/> match id.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public Task<List<string>> GetMatchesIdFromSummoner(string summonerPuuid, int nbMatchesToFetch = 20);

    /// <summary>
    /// Get a <see cref="MatchResponse"/> filled from the match id
    /// </summary>
    /// <param name="matchId"></param>
    /// <returns>a <see cref="MatchResponse"/></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ArgumentException"></exception>
    public Task<MatchResponse> GetMatchDetailsFromId(string matchId);

    /// <summary>
    /// Adds a MatchResponse to Database
    /// </summary>
    /// <param name="Match"></param>
    /// <returns></returns>
    public Task<MatchResponse> AddMatchToDataBase(MatchResponse Match);

    /// <summary>
    /// Returns the List of MatchResponse of all Matches in Database that the summoner has played
    /// </summary>
    /// <param name="puuid"> A <see cref="System.String"/> who contains a puuid for the desired summoner. </param>
    /// <returns>A <see cref="List{T}"/> of <see cref="MatchResponse"/> in the database of the summoner</returns>
    public Task<List<MatchResponse>> GetSummonnerMatchesFromDataBase(string puuid);
}
