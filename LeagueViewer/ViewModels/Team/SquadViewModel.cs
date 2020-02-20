using LeagueViewer.Models;
using System.Collections.Generic;

namespace LeagueViewer.ViewModels.Team
{
    public class SquadViewModel
    {
        public Models.Team Team { get; set; }
        public Models.League League { get; set; }
        public IEnumerable<PlayerExtended> Players { get; set; }
    }
}
