// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : SummonerResponse.cs
//           Response of a unique summoner with added List<LeagueEntry>

using tft_module.Models.Dto;
using tft_module.Models.Dto.LeagueEntryDto;

namespace tft_module.Models.Response;

public class SummonerResponse : SummonerDto
{
    public List<LeagueEntryDto> LeagueEntry { get; set; }
}
