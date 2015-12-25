using System.Diagnostics;
using System.Threading.Tasks;

using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Models;

namespace Vulpecula.Mobile.Models
{
    public class CroudiaProvider
    {
        private readonly IConstants _constants;

        public Croudia Croudia { get; }

        public User User { get; private set; }

        public CroudiaProvider(string consumerKey, string consumerSecret)
        {
            this.Croudia = new Croudia(consumerKey, consumerSecret);
            this._constants = App.ModelLocator.GetModel<IConstants>();
        }

        public bool ReAuthorization(IPasswordCredentials credentials)
        {
            var vault = App.ModelLocator.GetModel<IPasswordVault>();
            if (credentials != null)
            {
                this.Croudia.RefreshToken = credentials.Password;
                try
                {
                    this.Croudia.OAuth.Refresh();
                    var task = Task.Run(() =>
                        {
                            return this.Croudia.Account.VerifyCredentials();
                        });
                    if (!task.Wait(1000 * 5))
                    {
                        vault.Remove(credentials);
                        Debug.WriteLine("Authorization timed out. Account information is deleted.");
                        return false;
                    }
                    this.User = task.Result;

                    var newCredentials = App.ModelLocator.GetModel<IPasswordCredentials>();
                    newCredentials.UserName = credentials.UserName;
                    newCredentials.Password = this.Croudia.RefreshToken;
                    vault.Update(credentials, newCredentials);
                    return true;
                }
                catch
                {
                    vault.Remove(credentials);
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> Authorization(string url)
        {
            if (!url.StartsWith(_constants.RedirectUrl))
            {
                return false;
            }

            try
            {
                var code = url.Replace(_constants.RedirectUrl + "?code=", "");
                await this.Croudia.OAuth.TokenAsync(code);
                this.User = await this.Croudia.Account.VerifyCredentialsAsync();

                var vault = App.ModelLocator.GetModel<IPasswordVault>();
                var credentials = App.ModelLocator.GetModel<IPasswordCredentials>();
                credentials.UserName = this.User.ScreenName;
                credentials.Password = this.Croudia.RefreshToken;
                vault.Add(credentials);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}