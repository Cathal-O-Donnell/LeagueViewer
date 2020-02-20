using LeagueViewer.Models;
using LeagueViewer.Services;
using System.Linq;
using Xunit;

namespace LeagueViewer.Tests.ServiceTests
{
    public class TeamServiceTests
    {
        private ITeamService _teamService;

        private void Init()
        {
            _teamService = new Services.TeamService();
        }

        [Fact]
        public async void GetLeagueTeams()
        {
            // Arrange
            Init();
            var teams = Enumerable.Empty<Team>();

            // Act
            teams = await _teamService.GetLeagueTeams(1);

            // Assert
            Assert.Equal(5, teams.Count());
        }

        [Fact]
        public async void GetTeam()
        {
            // Arrange
            Init();

            // Act
            var team = await _teamService.GetTeam(1);

            // Assert
            Assert.NotNull(team);
        }
    }
}
