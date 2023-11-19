// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : InfoDto.cs
//           Contains a Info Data Object from Riot Api

using Newtonsoft.Json;

namespace tft_module.Models.Dto.MatchDto;

public class InfoDto
{
    [JsonProperty("Game_Datetime")]  public long Game_Datetime { get; set; }
    [JsonProperty("Game_Length")] public float Game_Length { get; set; }
    [JsonProperty("Game_Variation")] public string Game_Variation { get; set; } = null!;
    [JsonProperty("Game_Version")] public string Game_Version { get; set; } = null!;
    [JsonProperty("Participants")] public List<ParticipantDto> Participants { get; set; } = null!;
    [JsonProperty("Queue_Id")] public int Queue_Id { get; set; }
    [JsonProperty("Tft_Set_Number")] public int Tft_Set_Number { get; set; }
}
