using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

// ReSharper disable InconsistentNaming

namespace Vulpecula.Rest
{
    public class OAuth : CroudiaApiImpl
    {
        internal OAuth(Croudia croudia) : base(croudia) {}

        /// <summary>
        /// 認証用 URL を取得します。
        /// </summary>
        /// <returns></returns>
        public string GetAuthorizeUrl(string state = null)
        {
            if (string.IsNullOrWhiteSpace(state))
                return $"{EndPoints.OAuth2Authorize}?response_type=code&client_id={Croudia.ConsumerKey}";
            else
            {
                return
                $"{EndPoints.OAuth2Authorize}?response_type=code&client_id={Croudia.ConsumerKey}&state={state}";
            }
        }

        /// <summary>
        /// アクセストークンを更新します。
        /// </summary>
        /// <returns></returns>
        public async Task<Token> TokenAsync(string access_code)
        {
            var token = await Croudia.PostAsync<Token>(EndPoints.OAuth2Token, grant_type => "authorization_code",
                                                       client_id => Croudia.ConsumerKey, client_secret => Croudia.ConsumerSecret, code => access_code);
            Croudia.SetTokens(token);
            return token;
        }

        /// <summary>
        /// アクセストークンを更新します。
        /// </summary>
        /// <returns></returns>
        public Token Token(string access_code)
        {
            var task = Task.Run(async () => await TokenAsync(access_code));
            task.Wait();
            Croudia.SetTokens(task.Result);
            return task.Result;
        }

        /// <summary>
        /// アクセストークンを更新します。
        /// </summary>
        /// <returns></returns>
        public async Task<Token> RefreshAsync()
        {
            var token = await Croudia.PostAsync<Token>(EndPoints.OAuth2Token, grant_type => "refresh_token",
                                                       client_id => Croudia.ConsumerKey, client_secret => Croudia.ConsumerSecret,
                                                       refresh_token => Croudia.RefreshToken);
            Croudia.SetTokens(token);
            return token;
        }

        /// <summary>
        /// アクセストークンを更新します。
        /// </summary>
        /// <returns></returns>
        public Token Refresh()
        {
            var task = Task.Run(async () => await RefreshAsync());
            task.Wait();
            Croudia.SetTokens(task.Result);
            return task.Result;
        }
    }
}