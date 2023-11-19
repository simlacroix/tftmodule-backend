// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : MiniSeriesDto.cs
//           Contains a Mini Series Data Object from Riot Api

namespace tft_module.Models.Dto.LeagueEntryDto;

public class MiniSeriesDto
{
    public int losses { get; set; }
    public int win { get; set; }
    public int target { get; set; }
    public string progress { get; set; }
}
