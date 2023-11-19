// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : MatchResponse.cs
//           Response of a unique match with added statistics

using MongoDB.Bson.Serialization.Attributes;
using tft_module.Models.Dto.MatchDto;

namespace tft_module.Models.Response;

public class MatchResponse : MatchDto
{
    [BsonId]
    public string Id { get; set; } = string.Empty;
    public List<SummonerResponse> SummonersParticipants { get; set; } = new List<SummonerResponse>();
    public bool MatchStatisticsCalculated { get; set; }
    public ParticipantDto focusedPlayer { get; set; }
}
