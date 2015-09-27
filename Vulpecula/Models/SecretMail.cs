using Newtonsoft.Json;

using Vulpecula.Models.Base;

namespace Vulpecula.Models
{
    public class SecretMail : StatusBase
    {
        [JsonProperty("recipient")]
        public User Recipient { get; set; }

        [JsonProperty("recipient_id")]
        public long RecipientId { get; set; }

        [JsonProperty("recipient_screen_name")]
        public string RecipientScreenName { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }

        [JsonProperty("sender_id")]
        public long SenderId { get; set; }

        [JsonProperty("sender_screen_name")]
        public string SenderScreenName { get; set; }
    }
}