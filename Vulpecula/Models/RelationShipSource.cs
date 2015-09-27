using Newtonsoft.Json;

namespace Vulpecula.Models
{
    public class RelationShipSource : RelationShipTarget
    {
        [JsonProperty("blocking")]
        public bool? IsBlocking { get; set; }

        [JsonProperty("muting")]
        public bool? IsMuting { get; set; }
    }
}