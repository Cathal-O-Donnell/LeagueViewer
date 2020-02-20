using LeagueViewer.Models;
using System.Collections.Generic;

namespace LeagueViewer.ViewModels.League
{
    public class DetailViewModel
    {
        public Models.League League { get; set; }
        public IEnumerable<Models.Fixture> PreviousFixtures { get; set; }
        public IEnumerable<Models.Fixture> UpcomingFixtures { get; set; }
        public IEnumerable<LeagueStanding> LeagueStandings { get; set; }
        public IEnumerable<LeagueNavigation> LeagueNavigation { get; set; }
        public IEnumerable<TopScorer> TopScorers { get; set; }
    }
}
