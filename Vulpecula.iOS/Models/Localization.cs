using Foundation;

using JetBrains.Annotations;

using Vulpecula.Mobile.Models.Interfaces;

namespace Vulpecula.iOS.Models
{
    [UsedImplicitly]
    public class Localization : ILocalization
    {
        public string GetString(string key)
        {
            return NSBundle.MainBundle.LocalizedString(key, key);
        }

        public string GetStringByFullName(string name)
        {
            return NSBundle.MainBundle.LocalizedString(name, name);
        }
    }
}