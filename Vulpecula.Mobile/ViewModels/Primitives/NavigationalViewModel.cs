using Prism.Navigation;

using Vulpecula.Mobile.Models.Interfaces;

namespace Vulpecula.Mobile.ViewModels.Primitives
{
    public class NavigationalViewModel : ViewModel, INavigationAware
    {
        public string NavigationTitle { get; set; }

        protected INavigationService NavigationService { get; }

        protected NavigationalViewModel(ILocalization localization, INavigationService navigationService)
        : base(localization)
        {
            NavigationService = navigationService;
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="parameters">The navigation parameters.</param>
        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="parameters">The navigation parameters.</param>
        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}