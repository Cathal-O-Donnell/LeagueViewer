using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueViewer.Models;

namespace LeagueViewer.Services
{
    public interface ILeagueService
    {
        Task<League> GetLeague(int id);
        Task<IEnumerable<League>> GetLeagueByCountry(string country, string year);
        Task<IEnumerable<LeagueNavigation>> GetLeagueNavigation();
        Task<IEnumerable<League>> GetCurrentLeagueByCountry(string country);
        Task<IEnumerable<League>> GetCurrentSeasonsAllLeagues();
        Task<IEnumerable<LeagueStanding>> GetLeagueStandings(int leagueId);
        Task<IEnumerable<Round>> GetAvailableRoundsForLeague(int leagueId);
        Task<Round> GetCurrentRoundForLeague(int leagueId);
        Task<IEnumerable<TopScorer>> GetLeageTopScorers(int leagueId);
    }
}