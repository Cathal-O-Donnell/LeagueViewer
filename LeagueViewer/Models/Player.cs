using Newtonsoft.Json;
using System;

namespace LeagueViewer.Models
{
    public class Player
    {
        [JsonProperty("player_id")]
        public int Id { get; set; }

        [JsonProperty("player_name")]
        public string Name { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("number")]
        public int? Number { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("birth_date")]
        public string DateOfBirth { get; set; }

        [JsonProperty("birth_place")]
        public string BirthPlace { get; set; }

        [JsonProperty("birth_country")]
        public string BirthCountry { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }
    }

    public class PlayerExtended : Player
    {
        [JsonProperty("injured")]
        public bool? IsInjured { get; set; }

        [JsonProperty("rating")]
        public decimal? Rating { get; set; }

        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        [JsonProperty("league")]
        public string LeagueName { get; set; }

        [JsonProperty("captain")]
        public int IsCaptain { get; set; }

        [JsonProperty("shots")]
        public PlayerStats.ShotStats Shots { get; set; }

        [JsonProperty("goals")]
        public PlayerStats.GoalStats Goals { get; set; }

        [JsonProperty("passes")]
        public PlayerStats.PassStats Passes { get; set; }

        [JsonProperty("tackles")]
        public PlayerStats.TackleStats Tackles { get; set; }

        [JsonProperty("duels")]
        public PlayerStats.DuelStats Duels { get; set; }

        [JsonProperty("dribbles")]
        public PlayerStats.DribbleStats Dribbles { get; set; }

        [JsonProperty("fouls")]
        public PlayerStats.FoulStats Fouls { get; set; }

        [JsonProperty("cards")]
        public PlayerStats.CardStats Cards { get; set; }

        [JsonProperty("penalty")]
        public PlayerStats.PenaltyStats Penalty { get; set; }

        [JsonProperty("games")]
        public PlayerStats.GameStats Games { get; set; }

        [JsonProperty("substitutes")]
        public PlayerStats.SubstitutionStats Substitutions { get; set; }
    }
}