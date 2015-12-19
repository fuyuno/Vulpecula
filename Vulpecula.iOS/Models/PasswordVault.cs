using System.Diagnostics;

using Foundation;

using Security;

using Vulpecula.iOS.Models;
using Vulpecula.Mobile.Models.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof (PasswordVault))]

namespace Vulpecula.iOS.Models
{
    public class PasswordVault : IPasswordVault
    {
        public void Add(IPasswordCredentials credentials)
        {
            var record = ((PasswordCredentials)credentials).AsSecRecord();
            record.Accessible = SecAccessible.Always;
            var code = SecKeyChain.Add(record);
            if (code != SecStatusCode.Success)
            {
                Debug.WriteLine($"Adding failure : {code}");
            }
        }

        public IPasswordCredentials FindByUserName(string username)
        {
            var query = new SecRecord(SecKind.InternetPassword)
            {
                Server = "croudia.com",
                Account = username
            };
            SecStatusCode code;
            var password = SecKeyChain.QueryAsData(query, false, out code);
            if (code != SecStatusCode.Success)
            {
                return null;
            }
            return new PasswordCredentials
            {
                UserName = username,
                Password = NSString.FromData(password, NSStringEncoding.UTF8)
            };
        }

        public void Remove(IPasswordCredentials credentials)
        {
            var code = SecKeyChain.Remove(((PasswordCredentials)credentials).AsSecRecord());
            if (code != SecStatusCode.Success)
            {
                Debug.WriteLine($"Remove failure : {code}");
            }
        }

        public void Update(IPasswordCredentials oldCredentials, IPasswordCredentials newCredentials)
        {
            var code = SecKeyChain.Remove(((PasswordCredentials)oldCredentials).AsSecRecord());
            if (code == SecStatusCode.Success)
            {
                var record = ((PasswordCredentials)newCredentials).AsSecRecord();
                record.Accessible = SecAccessible.Always;
                code = SecKeyChain.Add(record);
                if (code == SecStatusCode.Success)
                {
                    return;
                }
            }
            Debug.WriteLine($"Update failure : {code}");
        }
    }
}