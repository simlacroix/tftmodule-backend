// Project : TheTrackingFellowship
// Module  : Teamfight Tactics
// File    : TftController.cs
//           Controller who exposes the routes needed for the dashboard to request needed data

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using tft_module.Services;

namespace tft_module.Controllers;

[ApiController]
[Route("[controller]")]
public class TftController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ITftService _tftService;

    public TftController(ITftService tftService, ILogger<TftController> logger)
    {
        _tftService = tftService;
        _logger = logger;
    }

    /// <summary>
    /// Route : check-if-summoner-exists
    /// Returns a 200 if sumonner exists and a 400 if not.
    /// </summary>
    /// <param name="summonerName">A <see cref="System.String"/> that is the name of the account of the summoner.
    /// </param>
    /// <returns>
    /// Returns a 200 (< see cref="OkObjectResult"/>) if sumonner exists and a 400 < see cref="BadRequestObjectResult"/> if not.
    /// </returns>
    [HttpGet("check-if-summoner-exists")]
    public async Task<IActionResult> CheckIfSummonerExists(string summonerName)
    {
        try
        {
            return Ok(await _tftService.CheckIfSummonerExists(summonerName));
        }
        catch (Exception e)
        {
            _logger.LogInformation(e, $"exception while verifying if summoner: {summonerName} exists");
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Route : get-summoner-by-name
    /// Returns a 200 if sumonner exists and a 400 if not.
    /// </summary>
    /// <param name="summonerName">A <see cref="System.String"/> that is the name of the account of the summoner.
    /// </param>
    /// <returns>
    /// Returns a 200 (< see cref="OkObjectResult"/>) and an instance populated of <see cref="Models.Response.SummonerResponse"/> if sumonner exists and a 400 < see cref="BadRequestObjectResult"/> if not.
    /// </returns>
    [HttpGet("get-summoner-by-name")]
    public async Task<IActionResult> GetSummonerByName(string summonerName)
    {
        try
        {
            return Ok(await _tftService.GetSummonerByName(summonerName));
        }
        catch (Exception e)
        {
            _logger.LogInformation(e, $"exception while getting summoner: {summonerName}");
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Route : get-matches-for-summoner
    /// Returns a 200 if sumonner exists and a 400 if not.
    /// </summary>
    /// <param name="summonerName">A <see cref="System.String"/> that is the name of the account of the summoner.
    /// </param>
    /// <returns>
    /// Returns a 200 (< see cref="OkObjectResult"/>) if sumonner exists and a 400 < see cref="BadRequestObjectResult"/> if not.
    /// </returns>
    [HttpGet("get-matches-for-summoner")]
    public async Task<IActionResult>  GetMatchesForSummoner(string summonerName)
    {
        try
        {
            return Ok(SerializeResponse(await _tftService.GetMatchesForSummoner(summonerName)));
        }
        catch (Exception e)
        {
            _logger.LogInformation(e, $"Exception while getting matches for summoner: {summonerName}");
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Serialize a response to a Camel Case Naming Strategy (where possible)
    /// </summary>
    /// <param name="obj">A <see cref="System.Object"/> that is the name of the account of the summoner. </param>
    /// <returns>
    /// Returns the object serialized into a <see cref="System.String"/>
    /// </returns>
    private string SerializeResponse(Object obj)
    {
        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = false
            }
        };

        string json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.None
        });

        return json;
    }
}
