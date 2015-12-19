using Vulpecula.iOS.Models;
using Vulpecula.Mobile.Models.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof (Constants))]

namespace Vulpecula.iOS.Models
{
    public class Constants : IConstants
    {
        public string ConsumerKey => "a28fa2e2c4ed08378e8c6cfb71968bee0bb22a1d868cfa0b20a110a05af8476f";
        public string ConsumerSecret => "7204a8e17e7c1bae32e86c21b613d48d7acd7b54978bab54310c1775a0ca7146 ";
        public string RedirectUrl => "https://api.mkzk.xyz";
        public string AppKey => "xyz.mkzk.vulpecula";
    }
}