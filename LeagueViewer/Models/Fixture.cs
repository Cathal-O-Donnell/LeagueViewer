using Newtonsoft.Json;
using System.Globalization;
using System;
using System.Collections.Generic;

namespace LeagueViewer.Models
{
    public class Fixture
    {
        [JsonProperty("fixture_id")]
        public int Id { get; set; }

        [JsonProperty("league_id")]
        public int LeagueId { get; set; }

        [JsonProperty("event_date")]
        public string StartDate { get; set; }

        [JsonProperty("firstHalfStart")]
        public string FirstHalfStart { get; set; }

        [JsonProperty("secondHalfStart")]
        public string SecondHalfStart { get; set; }

        [JsonProperty("round")]
        public string Round { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("statusShort")]
        public string StatusShort { get; set; }

        [JsonProperty("elapsed")]
        public string Elapsed { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("referee")]
        public string Referee { get; set; }

        [JsonProperty("goalsHomeTeam")]
        public int? HomeScore { get; set; }

        [JsonProperty("goalsAwayTeam")]
        public int? AwayScore { get; set; }

        [JsonProperty("homeTeam")]
        public FixtureTeam HomeTeam { get; set; }

        [JsonProperty("awayTeam")]
        public FixtureTeam AwayTeam { get; set; }

        [JsonProperty("events")]
        public IEnumerable<FixtureEvent> FixtureEvents { get; set; }

        [JsonProperty("statistics")]
        public FixtureStatistics Statistics { get; set; }

        public DateTime? FixtureDate { get; set; }

        public void PopulateFixtureDate()
        {
            if (!String.IsNullOrEmpty(this.StartDate))
            {
                this.FixtureDate = DateTime.ParseExact(this.StartDate, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }
    }

    public class FixtureStatistics
    {
        [JsonProperty("Shots on Goal")]
        public FixtureStats.ShotsOnTarget ShotsOnTarget { get; set; }

        [JsonProperty("Shots off Goal")]
        public FixtureStats.ShotsOffTarget ShotsOffTarget { get; set; }

        [JsonProperty("Total Shots")]
        public FixtureStats.TotalShots TotalShots { get; set; }

        [JsonProperty("Fouls")]
        public FixtureStats.Fouls Fouls { get; set; }

        [JsonProperty("Yellow Cards")]
        public FixtureStats.YellowCards YellowCards { get; set; }

        [JsonProperty("Red Cards")]
        public FixtureStats.RedCards RedCards { get; set; }

        [JsonProperty("Ball Possession")]
        public FixtureStats.Possession Possession { get; set; }

        [JsonProperty("Total passes")]
        public FixtureStats.Passes Passes { get; set; }

        [JsonProperty("Passes accurate")]
        public FixtureStats.PassAccurate PassAccurate { get; set; }

        [JsonProperty("Offsides")]
        public FixtureStats.Offsides Offsides { get; set; }

        [JsonProperty("Corner Kicks")]
        public FixtureStats.Corners Corners { get; set; }

        public string PassAccuracyHome { get; set; }
        public string PassAccuracyAway { get; set; }

        public void CalculatePassAccuracy()
        {
            if (this.PassAccurate != null && this.Passes != null)
            {
                double passAccuracyHome = Convert.ToDouble(((double)this.PassAccurate.Home / (double)this.Passes.Home) * 100);
                PassAccuracyHome = $"{passAccuracyHome.ToString("#")}%";

                double passAccuracyAway = Convert.ToDouble(((double)this.PassAccurate.Away / (double)this.Passes.Away) * 100);
                PassAccuracyAway = $"{passAccuracyAway.ToString("#")}%";
            }
        }
    }

    public class FixtureEvent
    {
        [JsonProperty("elapsed")]
        public int Elapsed { get; set; }

        [JsonProperty("team_id")]
        public int TeamId { get; set; }

        [JsonProperty("teamName")]
        public string TeamName { get; set; }

        [JsonProperty("player_id")]
        public int? PlayerId { get; set; }

        [JsonProperty("player")]
        public string PlayerName { get; set; }

        [JsonProperty("assist_id")]
        public int? AssistId { get; set; }

        [JsonProperty("assist")]
        public string Assist { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        public string HtmlString { get; set; }
    }

    public class FixtureTeam
    {
        [JsonProperty("team_id")]
        public int Id { get; set; }

        [JsonProperty("team_name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        public string Code { get; set; }
    }
}
