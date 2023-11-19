// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : MetaDataDto.cs
//           Contains a MetaData Data Object from Riot Api

using Newtonsoft.Json;

namespace tft_module.Models.Dto.MatchDto;

public class MetaDataDto
{
    [JsonProperty("Data_Version")] public string Data_Version { get; set; } = null!;
    [JsonProperty("Match_Id")] public string Match_Id { get; set; } = null!;
    [JsonProperty("Participants")] public List<string> Participants { get; set; } = null!;
}
