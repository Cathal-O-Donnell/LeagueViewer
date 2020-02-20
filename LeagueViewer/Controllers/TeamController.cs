using LeagueViewer.Helpers;
using LeagueViewer.Models;
using LeagueViewer.Services;
using LeagueViewer.ViewModels.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace LeagueViewer.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<TeamController> _logger;
        private readonly ILeagueService _leagueService;
        private readonly ITeamService _teamService;
        private readonly IFixtureService _fixtureService;
        private readonly IPlayerService _playerService;

        public TeamController(
            AppSettings appSettings,
            ILogger<TeamController> logger,
            ILeagueService leagueService,
            ITeamService teamService,
            IFixtureService fixtureService,
            IPlayerService playerService)
        {
            _appSettings = appSettings;
            _logger = logger;
            _leagueService = leagueService;
            _teamService = teamService;
            _fixtureService = fixtureService;
            _playerService = playerService;
        }

        // Views
        public async Task<IActionResult> Details(int id, int leagueId)
        {
            try
            {
                var league = await _leagueService.GetLeague(leagueId);

                var viewModel = new DetailsViewModel()
                {
                    League = league
                };
                var leagueStandings = Enumerable.Empty<LeagueStanding>();
                var topScorers = Enumerable.Empty<TopScorer>();

                Task<Team> teamTask = _teamService.GetTeam(id);
                Task<IEnumerable<Fixture>> upcomingFixturesTask = _fixtureService.GetTeamUpcomingFixtures(id, _appSettings.PreviousFixturesDisplay);
                Task<IEnumerable<Fixture>> previousFixturesTask = _fixtureService.GetTeamPreviousFixtures(id, _appSettings.PreviousFixturesDisplay);
                Task<IEnumerable<LeagueStanding>> leagueStandingsTask = _leagueService.GetLeagueStandings(leagueId);
                Task<IEnumerable<PlayerExtended>> playerStatisticTask = _playerService.GetTeamPlayersStatisticsForSeason(league.SeasonString, id);

                var allTasks = new List<Task> { teamTask, upcomingFixturesTask, previousFixturesTask, leagueStandingsTask, playerStatisticTask };

                while (allTasks.Any())
                {
                    Task finished = await Task.WhenAny(allTasks);

                    if (finished == teamTask)
                        viewModel.Team = teamTask.Result;
                    else if (finished == upcomingFixturesTask)
                        viewModel.UpcomingFixtures = upcomingFixturesTask.Result;
                    else if (finished == previousFixturesTask)
                        viewModel.PreviousFixtures = previousFixturesTask.Result;
                    else if (finished == leagueStandingsTask)
                        viewModel.LeagueStanding = leagueStandingsTask.Result.Where(x => x.TeamId == id).FirstOrDefault();
                    else if (finished == playerStatisticTask)
                        viewModel.Players = playerStatisticTask.Result;

                    allTasks.Remove(finished);
                }

                // Format data
                viewModel.Players = viewModel.Players
                    .Where(x => x.LeagueName == league.Name)
                    .OrderByDescending(x => x.Goals.Total)
                    .ThenByDescending(y => y.Games.MinutesPlayed)
                    .ToList();

                viewModel.LeagueStanding.Form = new LeagueHelper().GetTeamForm(viewModel.LeagueStanding.Forme);
                viewModel.PreviousFixtures = viewModel.PreviousFixtures.OrderBy(f => f.FixtureDate);
                viewModel.UpcomingFixtures = viewModel.UpcomingFixtures.OrderBy(f => f.FixtureDate);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured, see inner exception for more details", ex);
                return View();
            }
        }

        public async Task<IActionResult> Squad(int teamId, int leagueId)
        {
            try
            {
                var viewModel = new SquadViewModel();

                var teamTask = _teamService.GetTeam(teamId);
                var leagueTask = _leagueService.GetLeague(leagueId);
                var playersTask = _playerService.GetTeamPlayersStatisticsForSeason(leagueTask.Result.SeasonString, teamId);

                var allTasks = new List<Task> { teamTask, leagueTask, playersTask };

                while (allTasks.Any())
                {
                    Task finished = await Task.WhenAny(allTasks);

                    if (finished == leagueTask)
                        viewModel.League = leagueTask.Result;
                    else if (finished == teamTask)
                        viewModel.Team = teamTask.Result;
                    else if (finished == playersTask)
                        viewModel.Players = playersTask.Result;

                    allTasks.Remove(finished);
                }

                viewModel.Players = viewModel.Players.Where(x => x.LeagueName == viewModel.League.Name)
                        .OrderByDescending(x => x.Goals.Total)
                        .ThenByDescending(y => y.Games.MinutesPlayed)
                        .ToList();

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured, see inner exception for more details", ex);
                return View();
            }
        }
    }
}