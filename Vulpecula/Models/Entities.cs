using Newtonsoft.Json;

namespace Vulpecula.Models
{
    public class Entities
    {
        [JsonProperty("media")]
        public Media Media { get; set; }
    }
}