// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : MatchDto.cs
//           Contains a Match Data Object from Riot Api

using Newtonsoft.Json;

namespace tft_module.Models.Dto.MatchDto;

public class MatchDto
{
    [JsonProperty("Metadata")] public MetaDataDto Metadata { get; set; } = null!;
    [JsonProperty("Info")] public InfoDto Info { get; set; } = null!;
}
