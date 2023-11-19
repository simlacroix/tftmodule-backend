// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : MatchesFromQueueAndTagResponse.cs
//           Response returns from a request to fetch matches from a summoner

namespace tft_module.Models.Response;

public class MatchesFromQueueAndTagResponse
{
    public List<MatchResponse> Matches { get; set; } = new List<MatchResponse>();
    public float AveragePlacement { get; set; }
    public float AverageDamageToPlayers { get; set; }
}