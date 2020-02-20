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
    public class SeasonService : ISeasonService
    {
        private readonly ILogger<SeasonService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        
        public SeasonService(
            IHttpClientFactory clientFactory,
            ILogger<SeasonService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<Season>> GetAvailableSeasons()
        {
            try
            {
                string requestUri = $@"v2/seasons";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<Season> seasons = Enumerable.Empty<Season>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["seasons"].Children().ToList();
                    
                    foreach (var result in results)
                    {
                        seasons = seasons.Append(
                            new Season()
                            {
                                Year = (int)result
                            }
                        );
                    }
                }

                return seasons.ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message.ToString());
                return null;
            }        
        }
    }
}