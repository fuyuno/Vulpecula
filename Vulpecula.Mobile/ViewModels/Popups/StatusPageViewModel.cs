using Prism.Navigation;

using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;

namespace Vulpecula.Mobile.ViewModels.Popups
{
    public class StatusPageViewModel : NavigationalViewModel
    {
        public StatusPageViewModel(ILocalization localization, INavigationService navigationService) : base(localization, navigationService)
        {
            NavigationTitle = "StatusPage";
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="parameters">The navigation parameters.</param>
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            MainText = "Hello !";
        }

        #region MainText

        private string _mainText;

        public string MainText
        {
            get { return _mainText; }
            set { this.SetProperty(ref _mainText, value); }
        }

        #endregion
    }
}