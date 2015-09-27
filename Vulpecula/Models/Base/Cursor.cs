using Newtonsoft.Json;

namespace Vulpecula.Models.Base
{
    public class Cursor
    {
        [JsonProperty("next_cursor")]
        public long NextCursor { get; set; }

        [JsonProperty("next_cursor_str")]
        public string NextCursorStr { get; set; }

        [JsonProperty("previous_cursor")]
        public long PreviousCursor { get; set; }

        [JsonProperty("previous_cursor_str")]
        public string PreviousCursorStr { get; set; }
    }
}