using Newtonsoft.Json;

namespace Vulpecula.Models
{
    public class RelationShipOwner2
    {
        [JsonProperty("connection")]
        public string[] Connection { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}