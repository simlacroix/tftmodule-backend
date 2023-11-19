// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : UnitDto.cs
//           Contains a Unit Data Object from Riot Api

using Newtonsoft.Json;

namespace tft_module.Models.Dto.MatchDto;

public class UnitDto
{
    [JsonProperty("Items")] public List<string> Items { get; set; } = null!;
    [JsonProperty("Character_Id")] public string CharacterId { get; set; } = null!;
    [JsonProperty("ItemNames")] public List<string> ItemNames { get; set; } = null!;
    [JsonProperty("Chosen")] public string Chosen { get; set; } = null!;
    [JsonProperty("Name")] public string Name { get; set; } = null!;
    [JsonProperty("Rarity")] public int Rarity { get; set; }
    [JsonProperty("Tier")] public int Tier { get; set; }
}