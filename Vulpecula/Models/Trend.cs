using Newtonsoft.Json;

namespace Vulpecula.Models
{
    public class Trend
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("promoted_content")]
        public bool? IsPromoted { get; set; }
    }
}