using System.Collections.Generic;
using System.Linq;

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
            SecKeyChain.Add(((PasswordCredentials)credentials).AsSecRecord());
        }

        public IReadOnlyList<IPasswordCredentials> FindAllByUserName(string username)
        {
            var record = new SecRecord(SecKind.InternetPassword)
            {
                Service = new Constants().AppKey,
                Server = "croudia.com",
                Account = username,
                Accessible = SecAccessible.Always
            };
            SecStatusCode code;
            var secRecords = SecKeyChain.QueryAsRecord(record, int.MaxValue, out code);
            if (code != SecStatusCode.Success)
            {
                return new List<IPasswordCredentials>();
            }

            var list = secRecords.Select(secRecord => new PasswordCredentials(secRecord)).Cast<IPasswordCredentials>().ToList();
            return list.AsReadOnly();
        }

        public void Remove(IPasswordCredentials credentials)
        {
            SecKeyChain.Remove(((PasswordCredentials)credentials).AsSecRecord());
        }

        public void Update(IPasswordCredentials oldCredentials, IPasswordCredentials newCredentials)
        {
            SecKeyChain.Update(((PasswordCredentials)oldCredentials).AsSecRecord(), ((PasswordCredentials)newCredentials).AsSecRecord());
        }
    }
}