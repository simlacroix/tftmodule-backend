// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : MatchRepository.cs
//           Basic methods for regualr use of the MongoDB TftMatches Collection

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using tft_module.Models;
using tft_module.Models.Response;

namespace tft_module.Repositories.Impl;

public class MatchRepository : Repository, IMatchRepository
{
    private readonly IMongoCollection<MatchResponse> _matchesCollection;

    public MatchRepository(IOptions<TftDatabaseSettings> tftDatabaseSettings) : base(tftDatabaseSettings)
    {
        _matchesCollection =
            MongoDatabase.GetCollection<MatchResponse>(tftDatabaseSettings.Value.TftMatchesCollectionName);
    }

    /// <summary>
    /// Creates one instance of a match in the TftMatches MongoDB Collection
    /// </summary>
    /// <param name="matchResponse">An instance of <see cref="MatchResponse"/> </param>
    /// <returns></returns>
    public async Task Create(MatchResponse matchResponse)
    {
        await _matchesCollection.InsertOneAsync(matchResponse);
    }

    /// <summary>
    /// Gets a single match from its match_id
    /// </summary>
    /// <param name="matchId"> A <see cref="System.String"/> who contains a match_id. </param>
    /// <returns>An instance of a MatchResponse if the match is in the database or null if not in the database.</returns>
    public async Task<MatchResponse> GetMatchDetailFromId(string matchId)
    {
        return await _matchesCollection.Find(x => x.Id == matchId).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Returns the List of MatchResponse of all Matches in Database that the summoner has played
    /// </summary>
    /// <param name="puuid"> A <see cref="System.String"/> who contains a puuid for the desired summoner. </param>
    /// <returns>A <see cref="List{T}"/> of <see cref="MatchResponse"/> in the database of the summoner</returns>
    public async Task<List<MatchResponse>> GetMatchesDetailForSummoner(string puuid)
    {
        var filter = Builders<MatchResponse>.Filter.ElemMatch(x => x.Info.Participants, x => x.Puuid == puuid);
        return await _matchesCollection.Find(filter).ToListAsync();
    }
}
