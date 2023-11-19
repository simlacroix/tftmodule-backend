// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : TftService.cs
//           Parent Service to use both matches and summoner services

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Logging;
using tft_module.Exceptions;
using tft_module.Models.Dto;
using tft_module.Models.Response;

namespace tft_module.Services.Impl;

public class TftService : ITftService
{
    private readonly ILogger _logger;
    private readonly IMatchesService _matchesService;
    private readonly ISummonerService _summonerService;

    public TftService(ISummonerService summonerService, IMatchesService matchesService, ILogger<TftService> logger)
    {
        _summonerService = summonerService;
        _matchesService = matchesService;
        _logger = logger;
    }

    /// <summary>
    /// Check if Summoner exists.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Returns the name or throws an exception if not existant.</returns>
    /// <exception cref="UserNotFoundException"></exception>
    public async Task<string> CheckIfSummonerExists(string name)
    {
        _logger.LogInformation($"Checking if username: {name} exists");
        
        var summoner = await GetSummonerByName(name);

        if (summoner is null)
            throw new UserNotFoundException($"Summoner name doesn't exist {name}");
        
        _logger.LogDebug($"Response: {summoner.Name}");
        return summoner.Name;
    }

    /// <summary>
    /// Get a number of matches for a summoner
    /// </summary>
    /// <param name="name"></param>
    /// <param name="nbMatchesToFetch"></param>
    /// <returns><see cref="MatchesFromQueueAndTagResponse"/></returns>
    public async Task<MatchesFromQueueAndTagResponse> GetMatchesForSummoner(string name, int nbMatchesToFetch = 20)
    {
        _logger.LogInformation($"Getting matches({nbMatchesToFetch}) for summoner: {name}");
        SummonerResponse summoner = await GetSummonerByName(name);

        var ids = await _matchesService.GetMatchesIdFromSummoner(summoner.Puuid, nbMatchesToFetch);
        var response = new MatchesFromQueueAndTagResponse();

        var list = await _matchesService.GetSummonnerMatchesFromDataBase(summoner.Puuid);

        foreach (MatchResponse m in list)
        {
            m.focusedPlayer = m.Info.Participants.Find(x => x.Puuid == summoner.Puuid);
            response.Matches.Add(m);
        }

        foreach (string id in ids) {
            if (response.Matches.Find(x => x.Id == id) is null)
            {
                MatchResponse match = await CalculateSingleMatchStatistics(await _matchesService.GetMatchDetailsFromId(id));
                match.focusedPlayer = match.Info.Participants.Find(x => x.Puuid == summoner.Puuid);

                response.Matches.Add(match);
            }
        }

        _logger.LogDebug($"Starting calculated stats");

        float total_placements = 0;
        float total_damage_delt = 0;
        
        foreach (MatchResponse match in response.Matches) {
            var participant = match.Info.Participants.Find(x => x.Puuid == summoner.Puuid)!;
            total_placements += participant.Placement;
            total_damage_delt += participant.Total_Damage_To_Players;
        }
        response.AveragePlacement = total_placements/response.Matches.Count;
        response.AverageDamageToPlayers = total_damage_delt / response.Matches.Count;


        _logger.LogDebug($"End of calculated stats");

        _logger.LogDebug($"Response - {response}");
        return response;
    }

    /// <summary>
    /// Get a summoner by its name
    /// </summary>
    /// <param name="name"></param>
    /// <returns>A<see cref="SummonerResponse"/> if summoner exists.</returns>
    public async Task<SummonerResponse> GetSummonerByName(string name)
    {
        _logger.LogInformation($"Getting summoner: {name}");
        var response = await _summonerService.GetSummonerByName(name);
        
        _logger.LogDebug($"Response - {response}" );
        return response;
    }

    /// <summary>
    /// Calculate statistics for a single match.
    /// </summary>
    /// <param name="Match"></param>
    /// <returns></returns>
    private async Task<MatchResponse> CalculateSingleMatchStatistics(MatchResponse Match)
    {
        if (!Match.MatchStatisticsCalculated)
        {
            for (int i = 0; i < Match.Metadata.Participants.Count; i++)
            {
                Match.SummonersParticipants.Add(await _summonerService.GetSummonerByPuuid(Match.Metadata.Participants[i]));
            }
            Match.MatchStatisticsCalculated = true;
            await _matchesService.AddMatchToDataBase(Match);
        }
        return Match;
    }
}
