// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : SummonerDto.cs
//           Contains a Summoner Data Object from Riot Api

namespace tft_module.Models.Dto;

public class SummonerDto
{
    public string Id { get; set; } = null!;
    public string AccountId { get; set; } = null!;
    public string Puuid { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int ProfileIconId { get; set; }
    public string RevisionDate { get; set; } = null!;
    public int SummonerLevel { get; set; }
}