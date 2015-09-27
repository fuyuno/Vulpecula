using Newtonsoft.Json;

namespace Vulpecula.Models
{
    public class RelationShip
    {
        [JsonProperty("source")]
        public RelationShipSource Source { get; set; }

        [JsonProperty("target")]
        public RelationShipTarget Target { get; set; }
    }
}