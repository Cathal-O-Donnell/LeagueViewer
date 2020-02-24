using LeagueViewer.Models;
using LeagueViewer.Services;
using LeagueViewer.ViewModels.League;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace LeagueViewer.Controllers
{
    public class LeagueController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly CurrentLeagueSeasons _currentLeagueSeasons;
        private readonly ILogger<LeagueController> _logger;
        private readonly ILeagueService _leagueService;
        private readonly IFixtureService _fixtureService;

        public LeagueController(
            AppSettings appSettings,
            CurrentLeagueSeasons currentLeagueSeasons,
            ILogger<LeagueController> logger,
            ILeagueService leagueService,
            IFixtureService fixtureService)
        {
            _appSettings = appSettings;
            _currentLeagueSeasons = currentLeagueSeasons;
            _logger = logger;
            _leagueService = leagueService;
            _fixtureService = fixtureService;
        }

        // Views
        public async Task<IActionResult> Detail(int? leagueId)
        {
            try
            {
                var viewModel = new DetailViewModel();
                leagueId = LeagueIdValidation(leagueId);

                Task<League> leagueTask = _leagueService.GetLeague((int)leagueId);
                Task<IEnumerable<LeagueStanding>> leagueStandingsTask = _leagueService.GetLeagueStandings((int)leagueId);
                Task<Round> currentRoundTask = _leagueService.GetCurrentRoundForLeague((int)leagueId);
                Task<IEnumerable<Fixture>> upcomingFixturesTask = _fixtureService.GetLeagueRoundFixtures((int)leagueId, currentRoundTask.Result.LeagueRound);
                Task<IEnumerable<Fixture>> previousFixturesTask = _fixtureService.GetPreviousFixturesFromLeague((int)leagueId, _appSettings.PreviousFixturesDisplay);
                Task<IEnumerable<TopScorer>> topScorersTask = _leagueService.GetLeageTopScorers((int)leagueId);
                Task<IEnumerable<LeagueNavigation>> leagueNavigationTask = _leagueService.GetLeagueNavigation();

                var allTasks = new List<Task> { leagueTask, leagueStandingsTask, currentRoundTask, upcomingFixturesTask, previousFixturesTask, topScorersTask, leagueNavigationTask };

                while (allTasks.Any())
                {
                    Task finished = await Task.WhenAny(allTasks);

                    if (finished == leagueTask)
                        viewModel.League = leagueTask.Result;
                    else if (finished == leagueStandingsTask)
                        viewModel.LeagueStandings = leagueStandingsTask.Result;
                    else if (finished == upcomingFixturesTask)
                        viewModel.UpcomingFixtures = upcomingFixturesTask.Result;
                    else if (finished == previousFixturesTask)
                        viewModel.PreviousFixtures = previousFixturesTask.Result;
                    else if (finished == topScorersTask)
                        viewModel.TopScorers = topScorersTask.Result;
                    else if (finished == leagueNavigationTask)
                        viewModel.LeagueNavigation = leagueNavigationTask.Result;

                    allTasks.Remove(finished);
                }

                viewModel.TopScorers = viewModel.TopScorers
                    .Take(_appSettings.TopScorersDisplay)
                    .OrderByDescending(x => x.GoalStats.Total)
                    .ThenByDescending(y => y.GameStats.MinutesPlayed)
                    .ToList();

                foreach (var player in viewModel.TopScorers)
                {
                    var team = viewModel.LeagueStandings
                        .Where(ls => ls.TeamId == player.TeamId)
                        .FirstOrDefault();

                    if (team != null)
                        player.TeamLogo = team.Logo;
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured, see inner exception for more details", ex);
                return View();
            }
        }

        // Private Methods
        private int LeagueIdValidation(int? leagueId)
        {
            if (leagueId == _currentLeagueSeasons.PremierLeague ||
                leagueId == _currentLeagueSeasons.LaLiga ||
                leagueId == _currentLeagueSeasons.Bundesliga ||
                leagueId == _currentLeagueSeasons.Eredivisie ||
                leagueId == _currentLeagueSeasons.Ligue1 ||
                leagueId == _currentLeagueSeasons.ScottishPremierLeague ||
                leagueId == _currentLeagueSeasons.SeriaA ||
                leagueId == _currentLeagueSeasons.SerieABrazil)
                return (int)leagueId;
            else
                return _appSettings.DefaultLeague;
        }
    }
}