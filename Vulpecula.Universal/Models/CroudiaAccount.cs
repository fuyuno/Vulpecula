using System;
using System.Threading.Tasks;

using Windows.Security.Authentication.Web;
using Windows.Security.Credentials;

using Vulpecula.Models;

namespace Vulpecula.Universal.Models
{
    public class CroudiaAccount
    {
        public CroudiaAccount()
        {
            Croudia = new Croudia(AppDefintions.ConsumerKey, AppDefintions.ConsumerSecret);
        }

        public Croudia Croudia { get; }

        public User User { get; private set; }

        public int Row { get; set; }

        public async Task<bool> Authorization(PasswordVault vault, PasswordCredential credential)
        {
            if (credential != null)
            {
                credential.RetrievePassword();
                Croudia.RefreshToken = credential.Password;
                try
                {
                    await Croudia.OAuth.RefreshAsync();
                    User = await Croudia.Account.VerifyCredentialsAsync();
                    vault.Add(new PasswordCredential(AppDefintions.VulpeculaAppKey, User.IdStr,
                                                     Croudia.RefreshToken));
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            var startUri = new Uri(Croudia.OAuth.GetAuthorizeUrl());
            var endUri = new Uri("https://vulpecula.mkzk.xyz/authorized");
            try
            {
                var result =
                await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);
                if (result.ResponseStatus != WebAuthenticationStatus.Success)
                    return false;

                var url = result.ResponseData;
                await Croudia.OAuth.TokenAsync(url.Substring(url.IndexOf("code=", StringComparison.Ordinal) + 5));
                User = await Croudia.Account.VerifyCredentialsAsync();

                vault.Add(new PasswordCredential(AppDefintions.VulpeculaAppKey, User.IdStr,
                                                 Croudia.RefreshToken));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}