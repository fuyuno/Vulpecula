using Newtonsoft.Json;

namespace Vulpecula.Models
{
    public class SearchMetadata
    {
        [JsonProperty("completed_in")]
        public long CompletedIn { get; set; }

        [JsonProperty("max_id")]
        public long MaxId { get; set; }

        [JsonProperty("max_id_str")]
        public string MaxIdStr { get; set; }

        [JsonProperty("since_id")]
        public long SinceId { get; set; }

        [JsonProperty("since_id_str")]
        public string SinceIdStr { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next_results")]
        public string NextResults { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("refresh_url")]
        public string RefreshUrl { get; set; }
    }
}