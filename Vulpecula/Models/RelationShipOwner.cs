using Newtonsoft.Json;

namespace Vulpecula.Models
{
    public class RelationShipOwner
    {
        [JsonProperty("relationships")]
        public RelationShip RelationShip { get; set; }
    }
}