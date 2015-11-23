using System.Threading.Tasks;

using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;

using Xamarin.Forms;

namespace Vulpecula.Mobile.ViewModels
{
    public class AuthorizationPageViewModel : ViewModel
    {
        private readonly AccountManager _accountManager;
        private readonly IConstants _constants;
        private readonly INavigationService _navigationService;
        public string Title { get; set; }

        public AuthorizationPageViewModel(ILocalization localization, INavigationService navigationService, IConstants constants, AccountManager accountManager) : base(localization)
        {
            this._navigationService = navigationService;
            this._accountManager = accountManager;
            this._constants = constants;

            this.Title = this.GetLocalizedString("Authorization with Croudia");

            var croudia = new Croudia(constants.ConsumerKey, constants.ConsumerSecret);
            this.Source = croudia.OAuth.GetAuthorizeUrl();
        }

        // form Code-behind
        public async Task WebViewNavigated(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.StartsWith(this._constants.RedirectUrl))
            {
                await this._accountManager.AddAccount(e.Url);
                if (this._accountManager.Providers.Count == 1)
                {
                    this._navigationService.GoBack();
                }
            }
        }

        #region Source

        private string _source;

        public string Source
        {
            get { return this._source; }
            set { this.SetProperty(ref this._source, value); }
        }

        #endregion
    }
}