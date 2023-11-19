// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : ParticipantDto.cs
//           Contains a Participant Data Object from Riot Api

using System.Collections.Generic;
using Newtonsoft.Json;

namespace tft_module.Models.Dto.MatchDto;

public class ParticipantDto
{
    [JsonProperty("Companion")] public CompanionDto Companion { get; set; } = null!;
    [JsonProperty("Gold_Left")] public int Gold_Left { get; set; }
    [JsonProperty("Last_Round")]  public int Last_Round { get; set; }
    [JsonProperty("Level")] public int Level { get; set; }
    [JsonProperty("Placement")] public int Placement { get; set; }
    [JsonProperty("Players_Eliminated")] public int Players_Eliminated { get; set; }
    [JsonProperty("Puuid")] public string Puuid { get; set; } = null!;
    [JsonProperty("Time_Eliminated")]  public float Time_Eliminated { get; set; }
    [JsonProperty("Total_Damage_To_Players")]  public int Total_Damage_To_Players { get; set; }
    [JsonProperty("Traits")] public List<TraitDto> Traits { get; set; } = null!;
    [JsonProperty("Units")] public List<UnitDto> Units { get; set; } = null!;
    [JsonProperty("Augments")] public List<string> Augments { get; set; } = null!;
}
