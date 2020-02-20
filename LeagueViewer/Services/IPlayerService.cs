using LeagueViewer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeagueViewer.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetTeamPlayers(string season, int teamId);
        Task<IEnumerable<PlayerExtended>> GetTeamPlayersStatisticsForSeason(string season, int teamId);
    }
}