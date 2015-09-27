using Newtonsoft.Json;

using Vulpecula.Models.Base;

// ReSharper disable InconsistentNaming

namespace Vulpecula.Models
{
    public class IDs : Cursor
    {
        [JsonProperty("ids")]
        public long[] Ids;
    }
}