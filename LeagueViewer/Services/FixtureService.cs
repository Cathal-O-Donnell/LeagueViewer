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
    public class FixtureService : IFixtureService
    {
        private readonly ILogger<FixtureService> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public FixtureService(
            IHttpClientFactory clientFactory,
            ILogger<FixtureService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<Fixture> GetFixture(int id)
        {
            try
            {
                string requestUri = $@"v2/fixtures/id/{id}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);
                    JToken result = resultJSON["api"]["fixtures"].Children().FirstOrDefault();

                    var fixture = result.ToObject<Fixture>();
                    fixture.PopulateFixtureDate();

                    if (fixture.Statistics != null)
                        fixture.Statistics.CalculatePassAccuracy();

                    return fixture;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather fixture data");
                return null;
            }
        }

        public async Task<IEnumerable<Fixture>> GetPreviousFixturesFromLeague(int leagueId, int numberOfFixtures)
        {
            try
            {
                string requestUri = $@"v2/fixtures/league/{leagueId}/last/{numberOfFixtures}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<Fixture> fixtures = Enumerable.Empty<Fixture>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["fixtures"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        var fixture = result.ToObject<Fixture>();

                        fixture.PopulateFixtureDate();

                        if (fixture.Statistics != null)
                            fixture.Statistics.CalculatePassAccuracy();

                        fixtures = fixtures.Append(fixture);
                    }
                }

                fixtures = fixtures.OrderByDescending(f => f.FixtureDate);

                return fixtures.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather fixture data");
                return null;
            }
        }

        public async Task<IEnumerable<Fixture>> GetLeagueRoundFixtures(int leagueId, string round)
        {
            try
            {
                string requestUri = $@"v2/fixtures/league/{leagueId}/{round}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<Fixture> fixtures = Enumerable.Empty<Fixture>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["fixtures"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        var fixture = result.ToObject<Fixture>();

                        fixture.PopulateFixtureDate();

                        if (fixture.Statistics != null)
                            fixture.Statistics.CalculatePassAccuracy();

                        fixtures = fixtures.Append(fixture);
                    }
                }

                return fixtures.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather fixture data");
                return null;
            }
        }

        public async Task<IEnumerable<Fixture>> GetTeamPreviousFixtures(int teamId, int numberOfFixtures)
        {
            try
            {
                string requestUri = $@"v2/fixtures/team/{teamId}/last/{numberOfFixtures}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<Fixture> fixtures = Enumerable.Empty<Fixture>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["fixtures"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        var fixture = result.ToObject<Fixture>();

                        fixture.PopulateFixtureDate();

                        if (fixture.Statistics != null)
                            fixture.Statistics.CalculatePassAccuracy();

                        fixtures = fixtures.Append(fixture);
                    }
                }

                fixtures = fixtures.OrderByDescending(f => f.FixtureDate);

                return fixtures.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather fixture data");
                return null;
            }
        }

        public async Task<IEnumerable<Fixture>> GetTeamUpcomingFixtures(int teamId, int numberOfFixtures)
        {
            try
            {
                string requestUri = $@"v2/fixtures/team/{teamId}/next/{numberOfFixtures}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<Fixture> fixtures = Enumerable.Empty<Fixture>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["fixtures"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        var fixture = result.ToObject<Fixture>();

                        fixture.PopulateFixtureDate();

                        if (fixture.Statistics != null)
                            fixture.Statistics.CalculatePassAccuracy();

                        fixtures = fixtures.Append(fixture);
                    }
                }

                return fixtures.OrderByDescending(f => f.FixtureDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather fixture data");
                return null;
            }
        }

        public async Task<IEnumerable<Fixture>> GetLiveLeagueFixtures(int leagueId)
        {
            try
            {
                string requestUri = $@"v2/fixtures/league/{leagueId}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<Fixture> fixtures = Enumerable.Empty<Fixture>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["fixtures"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        var fixture = result.ToObject<Fixture>();

                        fixture.PopulateFixtureDate();

                        if (fixture.Statistics != null)
                            fixture.Statistics.CalculatePassAccuracy();

                        fixtures = fixtures.Append(fixture);
                    }
                }

                return fixtures.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather fixture data");
                return null;
            }
        }
    }
}
