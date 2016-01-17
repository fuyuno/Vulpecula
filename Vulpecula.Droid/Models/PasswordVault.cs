using Vulpecula.Droid.Models;
using Vulpecula.Mobile.Models.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof (PasswordVault))]

namespace Vulpecula.Droid.Models
{
    // TODO: Prerefenre -> String -> Key:Username, Value:Password
    public class PasswordVault : IPasswordVault
    {
        private readonly Configuration _configuration;

        public PasswordVault()
        {
            _configuration = new Configuration();
        }

        public void Add(IPasswordCredentials credentials)
        {
            _configuration.SetString(credentials.UserName, credentials.Password);
        }

        public IPasswordCredentials FindByUserName(string username)
        {
            var credentials = new PasswordCredentials
            {
                UserName = username,
                Password = _configuration.GetString(username)
            };
            return credentials;
        }

        public void Remove(IPasswordCredentials credentials)
        {
            _configuration.SetString(credentials.UserName, null);
        }

        public void Update(IPasswordCredentials oldCredentials, IPasswordCredentials newCredentials)
        {
            _configuration.SetString(oldCredentials.UserName, newCredentials.Password);
        }
    }
}