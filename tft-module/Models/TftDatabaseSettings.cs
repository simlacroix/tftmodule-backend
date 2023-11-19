// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : TftDatabaseSettings.cs
//           Settings for the Mongo Database

namespace tft_module.Models;

public class TftDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string TftMatchesCollectionName { get; set; } = null!;
    public string SummonerCollectionName { get; set; } = null!;
}
