// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : IMatchRepository.cs
//           Interface for the MatchRepository

using System.Threading.Tasks;
using tft_module.Models.Response;

namespace tft_module.Repositories;

public interface IMatchRepository
{
    /// <summary>
    /// Gets a single match from its match_id
    /// </summary>
    /// <param name="matchId"> A <see cref="System.String"/> who contains a match_id. </param>
    /// <returns>An instance of a MatchResponse if the match is in the database or null if not in the database.</returns>
    public Task<MatchResponse> GetMatchDetailFromId(string matchId);

    /// <summary>
    /// Creates one instance of a match in the TftMatches MongoDB Collection
    /// </summary>
    /// <param name="matchResponse">An instance of <see cref="MatchResponse"/> </param>
    /// <returns></returns>
    public Task Create(MatchResponse matchResponse);

    /// <summary>
    /// Returns the List of MatchResponse of all Matches in Database that the summoner has played
    /// </summary>
    /// <param name="puuid"> A <see cref="System.String"/> who contains a puuid for the desired summoner. </param>
    /// <returns>A <see cref="List{T}"/> of <see cref="MatchResponse"/> in the database of the summoner</returns>
    public Task<List<MatchResponse>> GetMatchesDetailForSummoner(string puuid);
}