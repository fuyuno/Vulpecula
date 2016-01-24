using System.Collections.ObjectModel;
using System.Windows.Input;

using JetBrains.Annotations;

using Prism.Commands;
using Prism.Windows.Navigation;

using Vulpecula.Models;
using Vulpecula.Universal.Helpers;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels
{
    [UsedImplicitly]
    public class MenuViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;

        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Accounts = new ObservableCollection<UserAccountViewModel>();
            EventFired = false;
            ViewModelHelper.SubscribeNotifyCollectionChanged(AccountManager.Instance.Users, Accounts, (User w) => UserAccountViewModel.Create(w));
        }

        #region Properties

        public ObservableCollection<UserAccountViewModel> Accounts { get; }

        #region EventFired

        private bool _eventFired;

        public bool EventFired
        {
            get { return _eventFired; }
            set { SetProperty(ref _eventFired, value); }
        }

        #endregion

        #endregion

        #region Commands

        #region AuthorizationCommand

        private ICommand _authorizationCommand;

        public ICommand AuthorizationCommand => _authorizationCommand ?? (_authorizationCommand = new DelegateCommand(Authorization));

        private async void Authorization() => await AccountManager.Instance.AuthorizationAccount();

        #endregion

        #region NavigateToHomePageCommand

        private ICommand _navigateToHomePageCommand;

        public ICommand NavigateToHomePageCommand
            => _navigateToHomePageCommand ?? (_navigateToHomePageCommand = new DelegateCommand(NavigateToHomePage));

        private void NavigateToHomePage()
        {
            // Navigate("Main", null) だと、死ぬっぽい
            _navigationService.Navigate("Main", null);
            EventFired = true;
        }

        #endregion

        #region NavigateToSettingsPageCommand

        private ICommand _navigateToSettingsPageCommand;

        public ICommand NavigateToSettingsPageCommand
            => _navigateToSettingsPageCommand ?? (_navigateToSettingsPageCommand = new DelegateCommand(NavigateToSettingsPage));

        private void NavigateToSettingsPage()
        {
            _navigationService.Navigate("Settings.SettingsMain", null);
            EventFired = true;
        }

        #endregion

        #endregion
    }
}