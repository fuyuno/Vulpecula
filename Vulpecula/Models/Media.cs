using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vulpecula.Models
{
    public class Media
    {
        [JsonProperty("media_url_https")]
        public string MediaUrlHttps { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MediaType Type { get; set; }
    }
}