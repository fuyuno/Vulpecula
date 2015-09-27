using Newtonsoft.Json;

using Vulpecula.Models.Base;

namespace Vulpecula.Models
{
    public class List : Cursor
    {
        [JsonProperty("users")]
        public User[] Users { get; set; }
    }
}