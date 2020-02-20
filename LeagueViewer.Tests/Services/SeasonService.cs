using LeagueViewer.Models;
using LeagueViewer.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeagueViewer.Tests.Services
{
    public class SeasonService : ISeasonService
    {
        public async Task<IEnumerable<Season>> GetAvailableSeasons()
        {
            return await GenerateTestSeasonData(5);
        }

        // Private methods
        private async Task<List<Season>> GenerateTestSeasonData(int itemsToReturn)
        {
            var seasons = new List<Season>();

            for (int i = 0; i < itemsToReturn; i++)
            {
                seasons.Add(new Season
                {
                    Year = DateTime.Now.Year
                });
            }

            return seasons;
        }
    }
}
