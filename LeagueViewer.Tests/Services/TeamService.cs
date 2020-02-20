using LeagueViewer.Models;
using LeagueViewer.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeagueViewer.Tests.Services
{
    class TeamService : ITeamService
    {
        public async Task<IEnumerable<Team>> GetLeagueTeams(int leagueId)
        {
            return await GenerateTestTeamData(5);
        }

        public async Task<Team> GetTeam(int id)
        {
            return new Team();
        }

        // Private methods
        private async Task<List<Team>> GenerateTestTeamData(int itemsToReturn)
        {
            var teams = new List<Team>();

            for (int i = 0; i < itemsToReturn; i++)
            {
                teams.Add(new Team { });
            }

            return teams;
        }
    }
}
