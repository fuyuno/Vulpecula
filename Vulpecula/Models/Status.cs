using Newtonsoft.Json;

using Vulpecula.Models.Base;

namespace Vulpecula.Models
{
    public class Status : StatusBase
    {
        [JsonProperty("favorited")]
        public bool IsFavorited { get; set; }

        [JsonProperty("favorites_count")]
        public long FavoritedCount { get; set; }

        [JsonProperty("spread")]
        public bool IsSpread { get; set; }

        [JsonProperty("spread_count")]
        public long SpreadCount { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public long? InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public string InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public long? InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public string InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("spread_status")]
        public Status SpreadStatus { get; set; }
    }
}