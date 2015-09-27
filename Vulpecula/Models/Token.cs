using System;

using Newtonsoft.Json;

namespace Vulpecula.Models
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        #region ExpiresIn

        private long _expiresIn;

        [JsonProperty("expires_in")]
        public long ExpiresIn
        {
            get { return _expiresIn; }
            set
            {
                if (_expiresIn == value)
                    return;
                _expiresIn = value;
                this.ExpiresInDateTime = DateTime.Now.AddSeconds(_expiresIn);
            }
        }

        [JsonIgnore]
        public DateTime ExpiresInDateTime { get; set; }

        #endregion

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}