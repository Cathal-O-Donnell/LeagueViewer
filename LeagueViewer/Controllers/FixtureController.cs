using System;
using System.Linq;
using System.Threading.Tasks;
using LeagueViewer.Helpers;
using LeagueViewer.Models;
using LeagueViewer.Services;
using LeagueViewer.ViewModels.Fixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LeagueViewer.Controllers
{
    public class FixtureController : Controller
    {
        private readonly ILogger<FixtureController> _logger;
        private readonly IFixtureService _fixtureService;
        private readonly ILeagueService _leagueService;
        private readonly ITeamService _teamService;

        public FixtureController(
            ILogger<FixtureController> logger,
            IFixtureService fixtureService,
            ILeagueService leagueService,
            ITeamService teamService)
        {
            _logger = logger;
            _fixtureService = fixtureService;
            _leagueService = leagueService;
            _teamService = teamService;
        }

        public async Task<IActionResult> Detail(int fixtureId, int? teamId)
        {
            try
            {
                var fixture = await _fixtureService.GetFixture(fixtureId);
                var league = await _leagueService.GetLeague(fixture.LeagueId);

                var viewModel = new DetailViewModel()
                {
                    League = league,
                    TeamId = teamId,
                    Fixture = fixture
                };

                if (viewModel.Fixture.FixtureEvents != null)
                {
                    viewModel.Fixture.FixtureEvents = viewModel.Fixture.FixtureEvents.OrderBy(x => x.Elapsed);

                    foreach (var fixtureEvent in viewModel.Fixture.FixtureEvents)
                    {
                        fixtureEvent.HtmlString = new FixtureHelper().GetFixtureEventHtml(fixtureEvent);
                    }
                }

                if (teamId.HasValue)
                    viewModel.Team = await _teamService.GetTeam((int)teamId);

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