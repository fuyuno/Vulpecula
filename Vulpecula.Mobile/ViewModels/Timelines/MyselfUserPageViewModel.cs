using Prism.Commands;
using Prism.Navigation;

using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.Views.Popups;

namespace Vulpecula.Mobile.ViewModels.Timelines
{
    public class MyselfUserPageViewModel : TabbedViewModel
    {
        public MyselfUserPageViewModel(ILocalization localization, INavigationService navigationService)
            : base(localization, navigationService)
        {
            Title = this.GetLocalizedString("Me");
            Icon = "user";
            NavigationTitle = this.GetLocalizedString("MePage");
        }

        #region NavigateCommand

        private DelegateCommand _navigateCommand;

        public DelegateCommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand(Navigate));

        private void Navigate()
        {
            NavigationService.Navigate<StatusPage>(useModalNavigation: true);
        }

        #endregion
    }
}