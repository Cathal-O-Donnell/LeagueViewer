using Newtonsoft.Json;

namespace LeagueViewer.Models
{
    public class TopScorer
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("player_name")]
        public string Name { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("team_id")]
        public int TeamId { get; set; }

        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        public Team Team { get; set; }

        public string TeamLogo { get; set; }

        [JsonProperty("games")]
        public PlayerStats.GameStats GameStats { get; set; }

        [JsonProperty("goals")]
        public PlayerStats.GoalStats GoalStats { get; set; }

        [JsonProperty("shots")]
        public PlayerStats.ShotStats ShotStats { get; set; }

        [JsonProperty("penalty")]
        public PlayerStats.PenaltyStats PenaltyStats { get; set; }
    }
}
