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
    public class PlayerService : IPlayerService
    {
        private readonly ILogger<PlayerService> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public PlayerService(
            IHttpClientFactory clientFactory,
            ILogger<PlayerService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<Player>> GetTeamPlayers(string season, int teamId)
        {
            try
            {
                string requestUri = $@"v2/players/squad/{teamId}/{season}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<Player> players = Enumerable.Empty<Player>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["players"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        players = players.Append(result.ToObject<Player>());
                    }
                }

                return players.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather league data");
                return null;
            }
        }

        public async Task<IEnumerable<PlayerExtended>> GetTeamPlayersStatisticsForSeason(string season, int teamId)
        {
            try
            {
                string requestUri = $@"v2/players/team/{teamId}/{season}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<PlayerExtended> players = Enumerable.Empty<PlayerExtended>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["players"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        players = players.Append(result.ToObject<PlayerExtended>());
                    }
                }

                return players.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather player statistic data");
                return null;
            }
        }
    }
}