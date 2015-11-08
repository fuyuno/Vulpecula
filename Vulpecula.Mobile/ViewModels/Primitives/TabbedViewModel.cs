using Prism.Navigation;

using Vulpecula.Mobile.Models.Interfaces;

namespace Vulpecula.Mobile.ViewModels.Primitives
{
    public class TabbedViewModel : NavigationalViewModel
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        protected TabbedViewModel(ILocalization localization, INavigationService navigationService) : base(localization, navigationService)
        {
        }
    }
}