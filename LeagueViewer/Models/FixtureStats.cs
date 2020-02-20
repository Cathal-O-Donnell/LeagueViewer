using Newtonsoft.Json;

namespace LeagueViewer.Models
{
    public class FixtureStats
    {
        public class ShotsOnTarget
        {
            [JsonProperty("home")]
            public int? Home { get; set; }

            [JsonProperty("away")]
            public int? Away { get; set; }
        }

        public class ShotsOffTarget
        {
            [JsonProperty("home")]
            public int? Home { get; set; }

            [JsonProperty("away")]
            public int? Away { get; set; }
        }

        public class TotalShots
        {
            [JsonProperty("home")]
            public int? Home { get; set; }

            [JsonProperty("away")]
            public int? Away { get; set; }
        }

        public class Fouls
        {
            [JsonProperty("home")]
            public int? Home { get; set; }

            [JsonProperty("away")]
            public int? Away { get; set; }
        }

        public class YellowCards
        {
            [JsonProperty("home")]
            public int? Home { get; set; }

            [JsonProperty("away")]
            public int? Away { get; set; }
        }

        public class RedCards
        {
            [JsonProperty("home")]
            public int? Home { get; set; }

            [JsonProperty("away")]
            public int? Away { get; set; }
        }

        public class Corners
        {
            [JsonProperty("home")]
            public int? Home { get; set; }

            [JsonProperty("away")]
            public int? Away { get; set; }
        }

        public class Offsides
        {
            [JsonProperty("home")]
            public int? Home { get; set; }

            [JsonProperty("away")]
            public int? Away { get; set; }
        }

        public class Passes
        {
            [JsonProperty("home")]
            public int? Home { get; set; }

            [JsonProperty("away")]
            public int? Away { get; set; }
        }

        public class PassAccurate
        {
            [JsonProperty("home")]
            public int? Home { get; set; }

            [JsonProperty("away")]
            public int? Away { get; set; }
        }

        public class Possession
        {
            [JsonProperty("home")]
            public string Home { get; set; }

            [JsonProperty("away")]
            public string Away { get; set; }
        }
    }
}
