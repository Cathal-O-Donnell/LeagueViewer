using LeagueViewer.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace LeagueViewer.Services
{
    public class CountryService : ICountryService
    {
        private readonly ILogger<CountryService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        
        public CountryService(
            IHttpClientFactory clientFactory,
            ILogger<CountryService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }
        public async Task<IEnumerable<Country>> GetCountries()
        {
            try
            {
                string requestUri = $@"v2/countries";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<Country> countries = Enumerable.Empty<Country>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["countries"].Children().ToList();
                    
                    foreach (JToken result in results)
                    {
                        countries = countries.Append(result.ToObject<Country>());
                    }
                }

                return countries.ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message.ToString());
                return null;
            }        
        }
    }
}