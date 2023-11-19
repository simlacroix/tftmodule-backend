// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : MatchesService.cs
//           Methods to get data from both database and Riot APIS

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using tft_module.Models.Response;
using tft_module.Repositories;

namespace tft_module.Services.Impl;

public class MatchesService : IMatchesService
{
    private readonly ILogger _logger;
    private readonly HttpClient _clientAmericas;
    private readonly IMatchRepository _matchRepository;

    public MatchesService(IMatchRepository matchRepository, ILogger<MatchesService> logger)
    {
        _matchRepository = matchRepository;
        _logger = logger;
        _clientAmericas = new HttpClient
        {
            BaseAddress = new Uri(Globals.BaseAmericaUrl)
        };
    }

    /// <summary>
    /// Get a number of latest match id for a summoner
    /// </summary>
    /// <param name="summonerPuuid"></param>
    /// <param name="nbMatchesToFetch"></param>
    /// <returns>A list of <see cref="System.String"/> match id.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<List<string>> GetMatchesIdFromSummoner(string summonerPuuid, int nbMatchesToFetch = 2)
    {
        _logger.LogInformation($"Getting matches({nbMatchesToFetch}) for summoner id: {summonerPuuid}");
        
        var callUrl =
            $"/tft/match/v1/matches/by-puuid/{summonerPuuid}/ids?start=0&count={nbMatchesToFetch}&api_key={Globals.ApiKey}";
        var response = _clientAmericas.GetAsync(callUrl).Result;

        List<string> ids;
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            ids = JsonConvert.DeserializeObject<List<string>>(content) ??
                  throw new InvalidOperationException("Riot API returned null on Match IDs");
        }
        else
        {
            throw new Exception($"Error : {response.StatusCode} : {response.RequestMessage}");
        }

        _logger.LogDebug($"Response - {ids}" );
        return ids;
    }

    /// <summary>
    /// Get a <see cref="MatchResponse"/> filled from the match id
    /// </summary>
    /// <param name="matchId"></param>
    /// <returns>a <see cref="MatchResponse"/></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ArgumentException"></exception>
    public async Task<MatchResponse> GetMatchDetailsFromId(string matchId)
    {
        _logger.LogInformation($"Getting matches details for match id: {matchId}");
        
        var match = await _matchRepository.GetMatchDetailFromId(matchId);

        if (match is null)
        {
            var fetch_data_again = false;

            do
            {
                fetch_data_again = false;
                var response = await _clientAmericas.GetAsync($"/tft/match/v1/matches/{matchId}?api_key={Globals.ApiKey}");

                if ((int)response.StatusCode == 429)
                {
                    fetch_data_again = true;
                    Thread.Sleep(1000);
                }

                if (!response.IsSuccessStatusCode && fetch_data_again)
                    throw new Exception($"An error has occured. Error : {response.StatusCode} : {response.RequestMessage}");

                var content = await response.Content.ReadAsStringAsync();

                var dezerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                };

                match = JsonConvert.DeserializeObject<MatchResponse>(content, dezerializerSettings);

                if (match is null) throw new ArgumentException($"No match found for id :  {matchId}");

                match.Id = matchId;



            } while (fetch_data_again);


        }
        _logger.LogDebug($"Response - {match}");
        return match;
    }

    /// <summary>
    /// Adds a MatchResponse to Database
    /// </summary>
    /// <param name="Match"></param>
    /// <returns></returns>
    public async Task<MatchResponse> AddMatchToDataBase(MatchResponse Match)
    {
        await _matchRepository.Create(Match);
        return Match;
    }

    /// <summary>
    /// Returns the List of MatchResponse of all Matches in Database that the summoner has played
    /// </summary>
    /// <param name="puuid"> A <see cref="System.String"/> who contains a puuid for the desired summoner. </param>
    /// <returns>A <see cref="List{T}"/> of <see cref="MatchResponse"/> in the database of the summoner</returns>
    public async Task<List<MatchResponse>> GetSummonnerMatchesFromDataBase(string puuid)
    {
        return await _matchRepository.GetMatchesDetailForSummoner(puuid);
    }

}
