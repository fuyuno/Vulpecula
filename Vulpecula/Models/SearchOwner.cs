using System.Collections.Generic;

using Newtonsoft.Json;

namespace Vulpecula.Models
{
    public class SearchOwner
    {
        [JsonProperty("statuses")]
        public IEnumerable<Status> Statuses { get; set; }

        [JsonProperty("search_meta")]
        public SearchMetadata Metadata { get; set; }
    }
}