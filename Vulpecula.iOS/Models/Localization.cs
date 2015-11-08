using Foundation;

using Vulpecula.iOS.Models;
using Vulpecula.Mobile.Models.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof (Localization))]

namespace Vulpecula.iOS.Models
{
    public class Localization : ILocalization
    {
        public string GetStringByFullName(string name)
        {
            return NSBundle.MainBundle.LocalizedString(name, name);
        }

        public string GetString(string key)
        {
            return NSBundle.MainBundle.LocalizedString(key, key);
        }
    }
}