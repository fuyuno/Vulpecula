using Foundation;

using Security;

using Vulpecula.iOS.Models;
using Vulpecula.Mobile.Models.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof (PasswordCredentials))]

namespace Vulpecula.iOS.Models
{
    public class PasswordCredentials : IPasswordCredentials
    {
        private readonly SecRecord _secRecord;

        // Used from Vulpecula.iOS
        internal PasswordCredentials(SecRecord record)
        {
            this._secRecord = record;
        }

        // Used from Vulpecula.Mobile
        public PasswordCredentials()
        {
            this._secRecord = new SecRecord(SecKind.InternetPassword) { Server = "croudia.com" };
        }

        public string UserName
        {
            get { return this._secRecord.Account; }
            set { this._secRecord.Account = value; }
        }

        public string Password
        {
            get { return NSString.FromData(this._secRecord.ValueData, NSStringEncoding.UTF8); }
            set { this._secRecord.ValueData = NSData.FromString(value, NSStringEncoding.UTF8); }
        }

        internal SecRecord AsSecRecord()
        {
            return _secRecord;
        }
    }
}