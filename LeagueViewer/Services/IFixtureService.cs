using LeagueViewer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeagueViewer.Services
{
    public interface IFixtureService
    {
        Task<Fixture> GetFixture(int id);
        Task<IEnumerable<Fixture>> GetTeamPreviousFixtures(int teamId, int numberOfFixtures);
        Task<IEnumerable<Fixture>> GetTeamUpcomingFixtures(int teamId, int numberOfFixtures);
        Task<IEnumerable<Fixture>> GetLeagueRoundFixtures(int leagueId, string round);
        Task<IEnumerable<Fixture>> GetLiveLeagueFixtures(int leagueId);
        Task<IEnumerable<Fixture>> GetPreviousFixturesFromLeague(int leagueId, int numberOfFixtures);
    }
}
