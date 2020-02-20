using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace LeagueViewer.Models
{
    public class LeagueStanding
    {
        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("team_id")]
        public int TeamId { get; set; }

        [JsonProperty("teamName")]
        public string TeamName { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("forme")]
        public string Forme { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("goalsDiff")]
        public int GoalsDiff { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("lastUpdate")]
        public DateTime LastUpdate { get; set; }

        [JsonProperty("all")]
        public AllGames All { get; set; }

        [JsonProperty("home")]
        public HomeGames Home { get; set; }

        [JsonProperty("away")]
        public AwayGames Away { get; set; }

        public string TableRowClass { get; set; }

        public List<Enums.Result> Form { get; set; }
    }

    public class AllGames
    {
        [JsonProperty("matchsPlayed")]
        public int MatchsPlayed { get; set; }

        [JsonProperty("win")]
        public int Win { get; set; }

        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("lose")]
        public int Lose { get; set; }

        [JsonProperty("goalsFor")]
        public int GoalsFor { get; set; }

        [JsonProperty("goalsAgainst")]
        public int GoalsAgaints { get; set; }
    }

    public class HomeGames
    {
        [JsonProperty("matchsPlayed")]
        public int MatchsPlayed { get; set; }

        [JsonProperty("win")]
        public int Win { get; set; }

        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("lose")]
        public int Lose { get; set; }

        [JsonProperty("goalsFor")]
        public int GoalsFor { get; set; }

        [JsonProperty("goalsAgainst")]
        public int GoalsAgaints { get; set; }
    }

    public class AwayGames
    {
        [JsonProperty("matchsPlayed")]
        public int MatchsPlayed { get; set; }

        [JsonProperty("win")]
        public int Win { get; set; }

        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("lose")]
        public int Lose { get; set; }

        [JsonProperty("goalsFor")]
        public int GoalsFor { get; set; }

        [JsonProperty("goalsAgainst")]
        public int GoalsAgaints { get; set; }
    }
}