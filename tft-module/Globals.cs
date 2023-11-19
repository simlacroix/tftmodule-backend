// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : Globals.cs
//           Constants needed by the whole application

namespace tft_module;

public static class Globals
{
    public const string BaseUrl = "https://na1.api.riotgames.com/";
    public const string BaseAmericaUrl = "https://americas.api.riotgames.com/";
    public static readonly string ApiKey = Environment.GetEnvironmentVariable("API_KEY_LOL") ?? throw new Exception("LOL API KEY NOT SET");
}
