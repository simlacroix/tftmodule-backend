// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : LeagueEntryDto.cs
//           Contains a leagueEntry Data Object from Riot Api

namespace tft_module.Models.Dto.LeagueEntryDto;

public class LeagueEntryDto
{
    public string? leagueId { get; set; }
    public string summonerId { get; set; }
    public string summonerName { get; set; }
    public string queueType { get; set; }
    public int? ratedTier { get; set; }
    public string? tier { get; set; }
    public string? rank { get; set; }
    public int? leaguePoints { get; set; }
    public int wins { get; set; }
    public int losses { get; set; }
    public bool? hotStreak { get; set; }
    public bool? veteran { get; set; }
    public bool? freshBlood { get; set; }
    public bool? inactive { get; set; }
    public MiniSeriesDto? miniSeries { get; set; }
}
