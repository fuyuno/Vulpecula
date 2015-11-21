using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;

namespace Vulpecula.Mobile.ViewModels
{
    public class AuthorizationPageViewModel : ViewModel
    {
        private readonly AccountManager _accountManager;
        public string Title { get; set; }

        public string Source { get; set; }

        public AuthorizationPageViewModel(ILocalization localization, AccountManager accountManager) : base(localization)
        {
            this._accountManager = accountManager;
            this.Title = this.GetLocalizedString("AuthorizationPage");
            this.Source = "https://croudia.com";
        }
    }
}