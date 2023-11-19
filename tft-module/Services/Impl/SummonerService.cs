// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : SummonerService.cs
//            Methods to get data from both database and Riot APIS

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using Microsoft.Extensions.Logging;
using tft_module.Models.Response;
using tft_module.Repositories;
using tft_module.Models.Dto.LeagueEntryDto;

namespace tft_module.Services.Impl;

public class SummonerService : ISummonerService
{
    private readonly ILogger _logger;
    private readonly HttpClient _client;
    private readonly ISummonerRepository _summonerRepository;

    public SummonerService(ISummonerRepository summonerRepository, ILogger<SummonerService> logger)
    {
        _summonerRepository = summonerRepository;
        _logger = logger;
        _client = new HttpClient();
        _client.BaseAddress = new Uri(Globals.BaseUrl);
    }

    /// <summary>
    /// Get a summoner by its name
    /// </summary>
    /// <param name="name"></param>
    /// <returns>A<see cref="SummonerResponse"/> if summoner exists.</returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ArgumentException"></exception>
    public async Task<SummonerResponse> GetSummonerByName(string name)
    {
        _logger.LogInformation($"Getting summoner: {name}");
        var playerAccount = await _summonerRepository.GetSummonerByName(name);

        if (playerAccount is null)
        {
            var response = await _client.GetAsync($"tft/summoner/v1/summoners/by-name/{name}?api_key={Globals.ApiKey}");

            if (!response.IsSuccessStatusCode)
                throw new Exception($"An error has occured. Error : {response.StatusCode} : {response.RequestMessage}");

            var content = await response.Content.ReadAsStringAsync();
            playerAccount = JsonConvert.DeserializeObject<SummonerResponse>(content);

            if (playerAccount is null) throw new ArgumentException($"No player account for username {name}");

            await _summonerRepository.Create(playerAccount);
        }

        var res = await _client.GetAsync($"/tft/league/v1/entries/by-summoner/{playerAccount.Id}?api_key={Globals.ApiKey}");
        if (!res.IsSuccessStatusCode)
            throw new Exception($"An error has occured. Error : {res.StatusCode} : {res.RequestMessage}");

        var contentLeagueEntry = await res.Content.ReadAsStringAsync();
        var leagueEntry = JsonConvert.DeserializeObject<List<LeagueEntryDto>>(contentLeagueEntry);

        playerAccount.LeagueEntry = leagueEntry;

        _logger.LogDebug($"Response - {playerAccount}" );
        return playerAccount;
    }

    /// <summary>
    /// Get a summoner by its puuid
    /// </summary>
    /// <param name="puuid"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ArgumentException"></exception>
    public async Task<SummonerResponse> GetSummonerByPuuid(string puuid)
    {
        var playerAccount = await _summonerRepository.GetSummonerByPuuid(puuid);

        if (playerAccount is null)
        {
            var response = await _client.GetAsync($"tft/summoner/v1/summoners/by-puuid/{puuid}?api_key={Globals.ApiKey}");

            if (!response.IsSuccessStatusCode)
                throw new Exception($"An error has occured. Error : {response.StatusCode} : {response.RequestMessage}");

            var content = await response.Content.ReadAsStringAsync();
            playerAccount = JsonConvert.DeserializeObject<SummonerResponse>(content);

            if (playerAccount is null) throw new ArgumentException($"No player account for puuid {puuid}");

            await _summonerRepository.Create(playerAccount);
        }

        return playerAccount;
    }

}