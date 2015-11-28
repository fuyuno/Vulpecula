using Vulpecula.Mobile.Models.Interfaces;

namespace Vulpecula.Droid.Models
{
    public class PasswordCredentials : IPasswordCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}