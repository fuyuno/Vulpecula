using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Vulpecula.Utilities;

namespace Vulpecula.Models
{
    public class Media
    {
        [JsonProperty("media_url_https")]
        public string MediaUrlHttps { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(MediaTypeConverter))]
        //[JsonConverter(typeof (StringEnumConverter))]
        public MediaType Type { get; set; }
    }
}