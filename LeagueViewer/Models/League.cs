using Newtonsoft.Json;
using System;

namespace LeagueViewer.Models
{
    public class League
    {
        [JsonProperty("league_id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("season_start")]
        public DateTime SeasonStart { get; set; }

        [JsonProperty("season_end")]
        public DateTime SeasonEnd { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("standings")]
        public bool Standings { get; set; }

        [JsonProperty("is_current")]
        public bool IsCurrent { get; set; }

        public string SeasonString { get; set; }

        public void PopulateSeasonString()
        {
            if (this.SeasonStart != null && this.SeasonEnd != null)
                this.SeasonString = $"{this.SeasonStart.ToString("yyyy")}-{this.SeasonEnd.ToString("yyyy")}";
        }
    }
}