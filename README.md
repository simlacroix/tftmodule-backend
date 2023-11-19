# Teamfight Tactics Module

## Name
**Teamfight Tactics Module** micro-services backend solution.

## Description
This is the backend solution for the Teamfight Tactics Module. It handles everything with 

## Installation
As the module is made into a Docker Image, simply run that image with the Docker-Compose or make sure all dependancies (such as MongoDB are up and running)
* Have Docker Desktop [install](https://www.docker.com/products/docker-desktop/)
* Set An Environment variable, make sure {username} and {password} correspond to the one use in the database. **THIS IS ON
  YOUR MACHINE**     
	* MONGODB_CONNECTION_STRING=mongodb://admin:pwd@localhost:27017/?authSource=admin

* Run a docker Container who is running a MongoDB image
	** Use the MongoDB init file to make sure you have all Collections ready
	Environment variables inside that container :
		*** MONGO_INITDB_ROOT_USERNAME: admin
        *** MONGO_INITDB_ROOT_PASSWORD: pwd
        *** MONGO_INITDB_DATABASE: TheTrackingFellowship

### Summary

| Service Name             | Protection | Port  |     
|--------------------------|------------|-------|
| Teamfight Tactics        | Private    | 3800  |     
| MongoDB                  | Private    | 27017 |     


## Usage
The purpose of this solution is to provide all information linked to Teamfight Tactics, such as matches and their statistics and the profile from Riot Account.

| Type  | Controller | Route                    | Request Model                                    | Response Model                                     |     
|-------|------------|--------------------------|--------------------------------------------------|----------------------------------------------------|
|  GET  |    TFT     | get-matches-for-summoner | String summonerName						        | [MatchesFromQueueAndTagResponse](#MatchesFromQueueAndTagResponse) |
|  GET  |    TFT     | get-summoner-by-name     | String summonerName						        | [SummonerResponse](#SummonerResponse) |
|  GET  |    TFT     | check-if-summoner-exists | String summonerName							    | String of username if exists |

### Response
#### MatchesFromQueueAndTagResponse

| Name                   | Type									  | Required |
|------------------------|----------------------------------------|----------|
| Matches				 | List<[MatchResponse](#MatchResponse)>  | true     |
| AveragePlacement		 | float								  | true     |
| AverageDamageToPlayers | float								  | true     |

#### MatchResponse (Dto : https://developer.riotgames.com/apis#tft-match-v1/GET_getMatch)

| Name						| Type											| Required |
|---------------------------|-----------------------------------------------|----------|
| Id					    | string										| true     |
| SummonersParticipants	    | List<[SummonerResponse](#SummonerResponse)>	| true     |
| MatchStatisticsCalculated | bool											| true     |
| focusedPlayer				| ParticipantDto								| true     |
| MetaData					| MetaData										| true     |
| Info						| Info											| true     |

#### SummonerResponse 
##### (SummonerDto : https://developer.riotgames.com/apis#tft-summoner-v1)
##### (LeagueEntryDto : https://developer.riotgames.com/apis#tft-league-v1/GET_getLeagueEntriesForSummoner)
| Name						| Type											| Required |
|---------------------------|-----------------------------------------------|----------|
| << SummonerDto fields >>  | SummonerDto									| true     |
| LeagueEntry				| List<LeagueEntryDto>							| true     |



## Authors and acknowledgment
### Authors
* Catherine Bronsard
* David Goulet-Paradis
* Simon Lacroix
* Antoine Toutant
### Acknowledgment
* Mikaël Fortin, Project Supervisor 

## License
### TODO
For open source projects, say how it is licensed.

## Project status
**In development**
