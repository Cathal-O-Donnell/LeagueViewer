using Newtonsoft.Json;

namespace LeagueViewer.Models
{
    public class PlayerStats
    {
        public class GameStats
        {
            [JsonProperty("appearences")]
            public int? Apperances { get; set; }

            [JsonProperty("minutes_played")]
            public int? MinutesPlayed { get; set; }
        }

        public class GoalStats
        {
            [JsonProperty("total")]
            public int? Total { get; set; }

            [JsonProperty("assists")]
            public int? Assists { get; set; }

            [JsonProperty("conceded")]
            public int? Conceded { get; set; }
        }

        public class ShotStats
        {
            [JsonProperty("total")]
            public int? Total { get; set; }

            [JsonProperty("on")]
            public int? OnTarget { get; set; }
        }

        public class PenaltyStats
        {
            [JsonProperty("won")]
            public int? Won { get; set; }

            [JsonProperty("commited")]
            public int? Commited { get; set; }

            [JsonProperty("success")]
            public int? Success { get; set; }

            [JsonProperty("missed")]
            public int? Missed { get; set; }

            [JsonProperty("saved")]
            public int? Saved { get; set; }
        }

        public class PassStats
        {
            [JsonProperty("total")]
            public int? Total { get; set; }

            [JsonProperty("accuracy")]
            public int? Accuracy { get; set; }
        }

        public class TackleStats
        {
            [JsonProperty("total")]
            public int? Total { get; set; }

            [JsonProperty("blocks")]
            public int? Blocks { get; set; }

            [JsonProperty("interceptions")]
            public int? Interceptions { get; set; }
        }

        public class DuelStats
        {
            [JsonProperty("total")]
            public int? Total { get; set; }

            [JsonProperty("won")]
            public int? Won { get; set; }
        }

        public class DribbleStats
        {
            [JsonProperty("attempts")]
            public int? Attempts { get; set; }

            [JsonProperty("success")]
            public int? Success { get; set; }
        }

        public class FoulStats
        {
            [JsonProperty("drawn")]
            public int? Drawn { get; set; }

            [JsonProperty("committed")]
            public int? Committed { get; set; }
        }

        public class SubstitutionStats
        {
            [JsonProperty("in")]
            public int? In { get; set; }

            [JsonProperty("out")]
            public int? Out { get; set; }

            [JsonProperty("bench")]
            public int? Bench { get; set; }
        }

        public class CardStats
        {
            [JsonProperty("yellow")]
            public int? Yellow { get; set; }

            [JsonProperty("yellowred")]
            public int? YellowRed { get; set; }

            [JsonProperty("red")]
            public int? Red { get; set; }
        }
    }
}
