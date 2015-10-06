using System;
using System.Linq;
using System.Threading.Tasks;

using Vulpecula.Models;

using Windows.Security.Authentication.Web;
using Windows.Security.Credentials;

namespace Vulpecula.Universal.Models
{
    public class CroudiaProvider
    {
        public Croudia Croudia { get; }

        public CroudiaProvider()
        {
            this.Croudia = new Croudia(AppDefintions.ConsumerKey, AppDefintions.ConsumerSecret);
        }

        public async Task<User> Authorization()
        {
            User user;
            var vault = new PasswordVault();
            var existing = vault.FindAllByResource(AppDefintions.VulpeculaAppKey).FirstOrDefault();
            if (existing != null)
            {
                existing.RetrievePassword();
                this.Croudia.RefreshToken = existing.Password;
                try
                {
                    await this.Croudia.OAuth.RefreshAsync();
                    user = await this.Croudia.Account.VerifyCredentialsAsync();

                    // 更新は、再度同じ Resource, Username で Add すれば良い
                    vault.Add(new PasswordCredential(AppDefintions.VulpeculaAppKey, user.ScreenName, this.Croudia.RefreshToken));
                    return user;
                }
                catch
                {
                    // ignored
                }
            }

            var startUri = new Uri(this.Croudia.OAuth.GetAuthorizeUrl());
            var endUri = new Uri("http://vulpecula.mkzk.tk/");
            var result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);
            if (result.ResponseStatus != WebAuthenticationStatus.Success)
                return null;

            var url = result.ResponseData;
            await this.Croudia.OAuth.TokenAsync(url.Substring(url.IndexOf("code=", StringComparison.Ordinal) + 5));
            user = await this.Croudia.Account.VerifyCredentialsAsync();

            vault.Add(new PasswordCredential(AppDefintions.VulpeculaAppKey, user.ScreenName, this.Croudia.RefreshToken));
            return user;
        }
    }
}