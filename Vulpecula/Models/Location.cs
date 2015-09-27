using Newtonsoft.Json;

namespace Vulpecula.Models
{
    public class Location
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("woeid")]
        public long Woeid { get; set; }
    }
}