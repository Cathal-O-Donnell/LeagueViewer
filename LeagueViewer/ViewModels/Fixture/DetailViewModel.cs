
namespace LeagueViewer.ViewModels.Fixture
{
    public class DetailViewModel
    {
        public int? TeamId { get; set; }
        public Models.League League { get; set; }
        public Models.Team Team { get; set; }
        public Models.Fixture Fixture { get; set; }
    }
}
