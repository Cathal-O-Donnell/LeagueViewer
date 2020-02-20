using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueViewer.Models;

namespace LeagueViewer.Services
{
    public interface ISeasonService
    {
         Task<IEnumerable<Season>> GetAvailableSeasons();         
    }
}