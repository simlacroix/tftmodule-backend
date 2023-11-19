// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : TraitDto.cs
//           Contains a Trait Data Object from Riot Api

using Newtonsoft.Json;

namespace tft_module.Models.Dto.MatchDto;

public class TraitDto
{
    [JsonProperty("Name")] public string Name { get; set; } = null!;
    [JsonProperty("Num_Units")] public int Num_Units { get; set; }
    [JsonProperty("Style")] public int Style { get; set; }
    [JsonProperty("Tier_Current")] public int Tier_Current { get; set; }
    [JsonProperty("Tier_Total")] public int Tier_Total { get; set; }
}