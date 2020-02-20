using LeagueViewer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeagueViewer.Services
{
    public interface ICountryService
    {
         Task<IEnumerable<Country>> GetCountries();
    }
}