using Newtonsoft.Json;

namespace LeagueViewer.Models
{
    public class Country
    {
        [JsonProperty("country")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set;}

        [JsonProperty("flag")]
        public string Flag { get; set; }
    }
}