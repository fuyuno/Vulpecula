using Vulpecula.Droid.Models;
using Vulpecula.Mobile.Models.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof (Constants))]
namespace Vulpecula.Droid.Models
{
    public class Constants : IConstants
    {
        public string ConsumerKey => "542c424cd0ddacedd2b63d444c462590da8706bf4a3cd6abdc12df34451f2606";
        public string ConsumerSecret => "14714def86ded5137d09fb335ef6e52d6e5ed3eab7de54e523b6ca2416d05313";
        public string RedirectUrl => "https://api.mkzk.xyz";
        public string AppKey => "xyz.mkzk.vulpecula";
    }
}