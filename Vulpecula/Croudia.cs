using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Vulpecula.Models;
using Vulpecula.Rest;
using Vulpecula.Rest.Internal;

// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable IntroduceOptionalParameters.Global

namespace Vulpecula
{
    public class Croudia
    {
        public string ConsumerKey { get; }

        public string ConsumerSecret { get; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public Account Account => new Account(this);

        public Blocks Blocks => new Blocks(this);

        public Favorites Favorites => new Favorites(this);

        public Followers Followers => new Followers(this);

        public Friends Friends => new Friends(this);

        public FriendShips FriendShips => new FriendShips(this);

        public Mutes Mutes => new Mutes(this);

        public OAuth OAuth => new OAuth(this);

        public Search Search => new Search(this);

        public SecretMails SecretMails => new SecretMails(this);

        public Statuses Statuses => new Statuses(this);

        public Trends Trends => new Trends(this);

        public Users Users => new Users(this);

        public Croudia(string consumerKey, string consumerSecret) : this(consumerKey, consumerSecret, "", "")
        {
        }

        public Croudia(string consumerKey, string consumerSecret, Token token)
            : this(consumerKey, consumerSecret, token.AccessToken, token.RefreshToken)
        {
        }

        public Croudia(string consumerKey, string consumerSecret, string accessToken, string refreshToken)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
        }

        public async Task<T> GetAsync<T>(string url, params Expression<Func<string, object>>[] parameters)
        {
            var param = parameters.Select(expression => new KeyValuePair<string, object>(expression.Parameters[0].Name, expression.Compile().Invoke(null))).ToList();
            return await this.GetAsync<T>(url, param);
        }

        private async Task<T> GetAsync<T>(string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (parameters != null)
                url += "?" + string.Join("&", parameters.Select(w => $"{w.Key}={w.Value}"));

            var httpClient = new HttpClient(new OAuth2ClientHandler(this));
            var responseString = await httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public async Task<T> PostAsync<T>(string url, params Expression<Func<string, object>>[] parameters)
        {
            var param = parameters.Select(expression => new KeyValuePair<string, object>(expression.Parameters[0].Name, expression.Compile().Invoke(null))).ToList();
            return await this.PostAsync<T>(url, param);
        }

        private async Task<T> PostAsync<T>(string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var httpClient = new HttpClient(new OAuth2ClientHandler(this));
            HttpContent content;
            if (parameters.Any(w => w.Key == "media" || w.Key == "image"))
            {
                content = new MultipartFormDataContent(new Guid().ToString().Replace("-", ""));
                foreach (var kvp in parameters)
                {
                    if (kvp.Key == "media" || kvp.Key == "image")
                    {
                        var streamContent = new StreamContent((Stream)kvp.Value);
                        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = $"\"{kvp.Key}\"",
                            FileName = $"\"{Path.GetRandomFileName()}\""
                        };
                        ((MultipartFormDataContent)content).Add(streamContent);
                    }
                    else
                    {
                        var stringContent =
                            new StringContent(kvp.Value is bool ? kvp.Value.ToString().ToLower() : kvp.Value.ToString());
                        stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = $"\"{kvp.Key}\"" };
                        ((MultipartFormDataContent)content).Add(stringContent);
                    }
                }
            }
            else
                content = new FormUrlEncodedContent(parameters.Select(w => new KeyValuePair<string, string>(w.Key, w.Value is bool ? w.Value.ToString().ToLower() : w.Value.ToString())));

            var response = await httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}