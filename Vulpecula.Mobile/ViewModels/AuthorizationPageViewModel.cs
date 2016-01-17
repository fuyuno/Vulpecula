using System.Threading.Tasks;

using JetBrains.Annotations;

using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;

using Xamarin.Forms;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Vulpecula.Mobile.ViewModels
{
    [UsedImplicitly]
    public class AuthorizationPageViewModel : ViewModel
    {
        private readonly AccountManager _accountManager;
        private readonly IConstants _constants;
        private readonly INavigationService _navigationService;

        public string Title { get; set; }

        public AuthorizationPageViewModel(ILocalization localization, INavigationService navigationService, IConstants constants, AccountManager accountManager)
        : base(localization)
        {
            _navigationService = navigationService;
            _accountManager = accountManager;
            _constants = constants;

            Title = GetLocalizedString("AuthorizationPage");

            var croudia = new Croudia(constants.ConsumerKey, constants.ConsumerSecret);
            Source = croudia.OAuth.GetAuthorizeUrl();
        }

        // form Code-behind
        public async Task WebViewNavigated(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.StartsWith(_constants.RedirectUrl))
            {
                await _accountManager.AddAccount(e.Url);
                if (_accountManager.Providers.Count == 1)
                    _navigationService.GoBack();
            }
        }

        #region Source

        private string _source;

        public string Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
        }

        #endregion
    }
}