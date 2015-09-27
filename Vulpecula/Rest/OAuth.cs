﻿using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

// ReSharper disable InconsistentNaming

namespace Vulpecula.Rest
{
    internal class OAuth : CroudiaApiImpl
    {
        public OAuth(Croudia croudia) : base(croudia)
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
        public async Task<Token> Token(string access_code)
        {
            return await this.Croudia.GetAsync<Token>(EndPoints.OAuth2Token, grant_type => "authorization_code",
                client_id => this.Croudia.ConsumerKey, cleint_secret => this.Croudia.ConsumerSecret, code => access_code);
        }

        /// <summary>
        /// アクセストークンを更新します。
        /// </summary>
        /// <returns></returns>
        public async Task<Token> Refresh()
        {
            return await this.Croudia.GetAsync<Token>(EndPoints.OAuth2Token, grant_type => "refresh_token",
                client_id => this.Croudia.ConsumerKey, client_secret => this.Croudia.ConsumerSecret,
                refresh_token => this.Croudia.RefreshToken);
        }
    }
}