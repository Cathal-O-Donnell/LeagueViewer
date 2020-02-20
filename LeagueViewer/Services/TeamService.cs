using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LeagueViewer.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace LeagueViewer.Services
{
    public class TeamService : ITeamService
    {
        private readonly ILogger<TeamService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        
        public TeamService(
            IHttpClientFactory clientFactory,
            ILogger<TeamService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<Team>> GetLeagueTeams(int leagueId)
        {
            try
            {
                string requestUri = $@"v2/teams/league/{leagueId}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<Team> teams = Enumerable.Empty<Team>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["teams"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        teams = teams.Append(result.ToObject<Team>());
                    }
                }

                return teams;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather league teams data");
                return null;
            }
        }

        public async Task<Team> GetTeam(int id)
        {
            try
            {
                string requestUri = $@"v2/teams/team/{id}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);
                    JToken result = resultJSON["api"]["teams"].Children().FirstOrDefault();
                    
                    return result.ToObject<Team>();
                }
                else                
                    return null;                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather team data");
                return null;
            }            
        }
    }
}