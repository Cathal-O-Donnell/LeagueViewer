using LeagueViewer.Models;
using LeagueViewer.Services;
using System.Linq;
using Xunit;

namespace LeagueViewer.Tests.ServiceTests
{
    public class SeasonServiceTests
    {
        private ISeasonService _seasonService;

        private void Init()
        {
            _seasonService = new Services.SeasonService();
        }

        [Fact]
        public async void GetAvailableSeasons()
        {
            // Arrange
            Init();
            var seasons = Enumerable.Empty<Season>();

            // Act
            seasons = await _seasonService.GetAvailableSeasons();

            // Assert
            Assert.Equal(5, seasons.Count());
        }
    }
}
