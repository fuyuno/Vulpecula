using Vulpecula.Droid.Models;
using Vulpecula.Mobile.Models.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof(PasswordCredentials))]

namespace Vulpecula.Droid.Models
{
    public class PasswordCredentials : IPasswordCredentials
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}