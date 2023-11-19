// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : CompanionDto.cs
//           Contains a Companion Data Object from Riot Api

using Newtonsoft.Json;

namespace tft_module.Models.Dto.MatchDto;

public class CompanionDto
{
    [JsonProperty("ContentId")] public string ContentId { get; set; } = null!;
    [JsonProperty("ItemId")] public int ItemId { get; set; }
    [JsonProperty("SkinId")] public int SkinId { get; set; }
    [JsonProperty("Species")] public string Species { get; set; } = null!;
}