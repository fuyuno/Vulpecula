using System;
using System.Threading.Tasks;

using Windows.Security.Authentication.Web;
using Windows.Security.Credentials;

using Vulpecula.Models;

namespace Vulpecula.Universal.Models
{
    public class CroudiaProvider
    {
        public Croudia Croudia { get; }

        public User User { get; private set; }

        public CroudiaProvider()
        {
            this.Croudia = new Croudia(AppDefintions.ConsumerKey, AppDefintions.ConsumerSecret);
        }

        public async Task<bool> Authorization(PasswordVault vault, PasswordCredential credential)
        {
            if (credential != null)
            {
                credential.RetrievePassword();
                this.Croudia.RefreshToken = credential.Password;
                try
                {
                    await this.Croudia.OAuth.RefreshAsync();
                    this.User = await this.Croudia.Account.VerifyCredentialsAsync();

                    // 更新は、再度同じ Resource, Username で Add すれば良い
                    vault.Add(new PasswordCredential(AppDefintions.VulpeculaAppKey, this.User.IdStr,
                        this.Croudia.RefreshToken));
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            var startUri = new Uri(this.Croudia.OAuth.GetAuthorizeUrl());
            var endUri = new Uri("https://vulpecula.mkzk.xyz/authorized");
            var result =
                await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);
            if (result.ResponseStatus != WebAuthenticationStatus.Success)
                return false;

            try
            {
                var url = result.ResponseData;
                await this.Croudia.OAuth.TokenAsync(url.Substring(url.IndexOf("code=", StringComparison.Ordinal) + 5));
                this.User = await this.Croudia.Account.VerifyCredentialsAsync();

                vault.Add(new PasswordCredential(AppDefintions.VulpeculaAppKey, this.User.IdStr,
                    this.Croudia.RefreshToken));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}