using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vulpecula.Models
{
    public class TrendOwner
    {
        [JsonProperty("as_of")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime AsOf { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("locations")]
        public Location Locations { get; set; }

        [JsonProperty("trends")]
        public Trend[] Trends { get; set; }
    }
}