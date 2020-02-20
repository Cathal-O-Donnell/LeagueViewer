using System.Collections.Generic;
using LeagueViewer.Models;

namespace LeagueViewer.ViewModels.Team
{
    public class DetailsViewModel
    {
        public Models.League League { get; set; }
        public Models.Team Team { get; set; }
        public IEnumerable<Models.Fixture> UpcomingFixtures { get; set; }
        public IEnumerable<Models.Fixture> PreviousFixtures { get; set; }
        public LeagueStanding LeagueStanding { get; set; }
        public IEnumerable<PlayerExtended> Players { get; set; }
        public IEnumerable<TopScorer> TopScorers { get; set; }
    }
}
