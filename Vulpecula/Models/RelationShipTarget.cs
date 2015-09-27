using Newtonsoft.Json;

namespace Vulpecula.Models
{
    public class RelationShipTarget
    {
        [JsonProperty("followed_by")]
        public bool IsFollowedBy { get; set; }

        [JsonProperty("following")]
        public bool IsFollowing { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}