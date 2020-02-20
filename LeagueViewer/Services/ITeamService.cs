using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueViewer.Models;

namespace LeagueViewer.Services
{
    public interface ITeamService
    {
        Task<Team> GetTeam(int id);
        Task<IEnumerable<Team>> GetLeagueTeams(int leagueId);        
    }
}