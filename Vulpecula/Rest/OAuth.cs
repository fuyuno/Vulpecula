using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

// ReSharper disable InconsistentNaming

namespace Vulpecula.Rest
{
    public class OAuth : CroudiaApiImpl
    {
        internal OAuth(Croudia croudia) : base(croudia)
        {
        }

        /// <summary>
        /// 認証用 URL を取得します。
        /// </summary>
        /// <returns></returns>
        public string GetAuthorizeUrl(string state = null)
        {
            if (string.IsNullOrWhiteSpace(state))
                return $"{EndPoints.OAuth2Authorize}?response_type=code&client_id={this.Croudia.ConsumerKey}";
            else
                return
                    $"{EndPoints.OAuth2Authorize}?response_type=code&client_id={this.Croudia.ConsumerKey}&state={state}";
        }

        /// <summary>
        /// アクセストークンを更新します。
        /// </summary>
        /// <returns></returns>
        public async Task<Token> TokenAsync(string access_code)
        {
            var token = await this.Croudia.PostAsync<Token>(EndPoints.OAuth2Token, grant_type => "authorization_code",
                client_id => this.Croudia.ConsumerKey, client_secret => this.Croudia.ConsumerSecret, code => access_code);
            this.Croudia.SetTokens(token);
            return token;
        }

        /// <summary>
        /// アクセストークンを更新します。
        /// </summary>
        /// <returns></returns>
        public Token Token(string access_code)
        {
            var task = Task.Run(async () =>
                await this.Croudia.PostAsync<Token>(EndPoints.OAuth2Token, grant_type => "authorization_code",
                    client_id => this.Croudia.ConsumerKey, client_secret => this.Croudia.ConsumerSecret,
                    code => access_code));
            task.Wait();
            this.Croudia.SetTokens(task.Result);
            return task.Result;
        }

        /// <summary>
        /// アクセストークンを更新します。
        /// </summary>
        /// <returns></returns>
        public async Task<Token> RefreshAsync()
        {
            var token = await this.Croudia.PostAsync<Token>(EndPoints.OAuth2Token, grant_type => "refresh_token",
                client_id => this.Croudia.ConsumerKey, client_secret => this.Croudia.ConsumerSecret,
                refresh_token => this.Croudia.RefreshToken);
            this.Croudia.SetTokens(token);
            return token;
        }

        /// <summary>
        /// アクセストークンを更新します。
        /// </summary>
        /// <returns></returns>
        public Token Refresh()
        {
            var task = Task.Run(async () =>
                await this.Croudia.PostAsync<Token>(EndPoints.OAuth2Token, grant_type => "refresh_token",
                    client_id => this.Croudia.ConsumerKey, client_secret => this.Croudia.ConsumerSecret,
                    refresh_token => this.Croudia.RefreshToken));
            task.Wait();
            this.Croudia.SetTokens(task.Result);
            return task.Result;
        }
    }
}