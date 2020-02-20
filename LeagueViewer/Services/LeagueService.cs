using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using LeagueViewer.Helpers;
using LeagueViewer.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace LeagueViewer.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly ILogger<LeagueService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly CurrentLeagueSeasons _currentLeagueSeasons;
        private readonly IMapper _mapper;

        public LeagueService(
            IHttpClientFactory clientFactory,
            ILogger<LeagueService> logger,
            CurrentLeagueSeasons currentLeagueSeasons,
            IMapper mapper)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _currentLeagueSeasons = currentLeagueSeasons;
            _mapper = mapper;
        }

        public async Task<League> GetLeague(int id)
        {
            try
            {
                string requestUri = $@"v2/leagues/league/{id}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);
                    JToken result = resultJSON["api"]["leagues"].Children().FirstOrDefault();

                    var league = result.ToObject<League>();
                    league.PopulateSeasonString();

                    return league;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather league data");
                return null;
            }
        }

        public async Task<IEnumerable<League>> GetLeagueByCountry(string country, string year)
        {
            try
            {
                string requestUri = $@"v2/leagues/country/{country}/{year}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<League> leagues = Enumerable.Empty<League>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["leagues"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        var league = result.ToObject<League>();
                        league.PopulateSeasonString();

                        leagues = leagues.Append(league);
                    }
                }

                return leagues.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather league data");
                return null;
            }
        }

        public async Task<IEnumerable<LeagueStanding>> GetLeagueStandings(int leagueId)
        {
            try
            {
                string requestUri = $@"v2/leagueTable/{leagueId}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<LeagueStanding> leagueStandings = Enumerable.Empty<LeagueStanding>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["standings"].Children().Children().ToList();

                    foreach (JToken result in results)
                    {
                        var leagueStanding = result.ToObject<LeagueStanding>();
                        leagueStanding.TableRowClass = new LeagueHelper().GetTableRowClass(leagueStanding.Description);
                        leagueStanding.Form = new LeagueHelper().GetTeamForm(leagueStanding.Forme);
                        leagueStandings = leagueStandings.Append(leagueStanding);
                    }
                }

                return leagueStandings.OrderBy(x => x.Rank);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<Round>> GetAvailableRoundsForLeague(int leagueId)
        {
            try
            {
                string requestUri = $@"v2/fixtures/rounds/{leagueId}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<Round> rounds = Enumerable.Empty<Round>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["fixtures"].Children().ToList();

                    foreach (var result in results)
                    {
                        rounds = rounds.Append(
                            new Round()
                            {
                                LeagueRound = (string)result
                            }
                        );
                    }
                }

                return rounds.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message.ToString());
                return null;
            }
        }

        public async Task<Round> GetCurrentRoundForLeague(int leagueId)
        {
            try
            {
                string requestUri = $@"v2/fixtures/rounds/{leagueId}/current";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                var response = await client.SendAsync(request);
                var currentRound = new Round();

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    JToken result = resultJSON["api"]["fixtures"].Children().FirstOrDefault();

                    currentRound = new Round()
                    {
                        LeagueRound = (string)result
                    };
                }

                return currentRound;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<League>> GetCurrentLeagueByCountry(string country)
        {
            try
            {
                string requestUri = $@"v2/leagues/current/{country}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<League> leagues = Enumerable.Empty<League>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["leagues"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        leagues = leagues.Append(result.ToObject<League>());
                    }
                }

                return leagues.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather league data");
                return null;
            }
        }

        public async Task<IEnumerable<TopScorer>> GetLeageTopScorers(int leagueId)
        {
            try
            {
                string requestUri = $@"/v2/topscorers/{leagueId}";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<TopScorer> topScorers = Enumerable.Empty<TopScorer>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["topscorers"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        topScorers = topScorers.Append(result.ToObject<TopScorer>());
                    }
                }

                return topScorers.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<LeagueNavigation>> GetLeagueNavigation()
        {
            var leagues = new List<League>();
            var leagueNavigations = new List<LeagueNavigation>();

            var premierLeagueTask = GetLeague(_currentLeagueSeasons.PremierLeague);
            var scottishPremierLeagueTask = GetLeague(_currentLeagueSeasons.ScottishPremierLeague);
            var bundesligaTask = GetLeague(_currentLeagueSeasons.Bundesliga);
            var serieATask = GetLeague(_currentLeagueSeasons.SeriaA);
            var eredivisieTask = GetLeague(_currentLeagueSeasons.Eredivisie);
            var serieABrazilTask = GetLeague(_currentLeagueSeasons.SerieABrazil);
            var ligue1Task = GetLeague(_currentLeagueSeasons.Ligue1);
            var laLigaTask = GetLeague(_currentLeagueSeasons.LaLiga);

            var allTasks = new List<Task> { premierLeagueTask, scottishPremierLeagueTask, bundesligaTask, serieATask, eredivisieTask, serieABrazilTask, ligue1Task, laLigaTask };

            while (allTasks.Any())
            {
                Task finished = await Task.WhenAny(allTasks);

                if (finished == premierLeagueTask)
                    leagues.Add(premierLeagueTask.Result);
                else if (finished == scottishPremierLeagueTask)
                    leagues.Add(scottishPremierLeagueTask.Result);
                else if (finished == bundesligaTask)
                    leagues.Add(bundesligaTask.Result);
                else if (finished == serieATask)
                    leagues.Add(serieATask.Result);
                else if (finished == eredivisieTask)
                    leagues.Add(eredivisieTask.Result);
                else if (finished == serieABrazilTask)
                    leagues.Add(serieABrazilTask.Result);
                else if (finished == ligue1Task)
                    leagues.Add(ligue1Task.Result);
                else if (finished == laLigaTask)
                    leagues.Add(laLigaTask.Result);

                allTasks.Remove(finished);
            }

            foreach (var league in leagues)
            {
                leagueNavigations.Add(_mapper.Map<League, LeagueNavigation>(league));
            }

            return leagueNavigations.OrderBy(l => l.Name);
        }

        public async Task<IEnumerable<League>> GetCurrentSeasonsAllLeagues()
        {
            try
            {
                string requestUri = $@"v2/leagues/current/";
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri);
                var client = _clientFactory.CreateClient("rapidFootball");

                IEnumerable<League> leagues = Enumerable.Empty<League>();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject resultJSON = JObject.Parse(responseString);

                    IList<JToken> results = resultJSON["api"]["leagues"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        leagues = leagues.Append(result.ToObject<League>());
                    }
                }

                return leagues.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to gather league data");
                return null;
            }
        }
    }
}