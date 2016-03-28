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
        public MenuViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Accounts = new ObservableCollection<UserAccountViewModel>();
            EventFired = false;
            ViewModelHelper.SubscribeNotifyCollectionChanged(AccountManager.Instance.Users, Accounts,
                                                             (User w) => UserAccountViewModel.Create(w));
        }

        protected INavigationService NavigationService { get; }

        #region Properties

        public ObservableCollection<UserAccountViewModel> Accounts { get; }

        #region EventFired

        private bool _eventFired;

        public bool EventFired
        {
            get { return _eventFired; }
            set { SetProperty(ref _eventFired, value); }
        }

        #endregion EventFired

        #endregion Properties

        #region Commands

        #region AuthorizationCommand

        private ICommand _authorizationCommand;

        public ICommand AuthorizationCommand
            => _authorizationCommand ?? (_authorizationCommand = new DelegateCommand(Authorization));

        private async void Authorization() => await AccountManager.Instance.AuthorizationAccount();

        #endregion AuthorizationCommand

        #region NavigateToHomePageCommand

        private ICommand _navigateToHomePageCommand;

        public ICommand NavigateToHomePageCommand
            => _navigateToHomePageCommand ?? (_navigateToHomePageCommand = new DelegateCommand(NavigateToHomePage));

        private void NavigateToHomePage()
        {
            NavigationService.Navigate("Main", null);
            EventFired = true;
        }

        #endregion NavigateToHomePageCommand

        #region NavigateToSettingsPageCommand

        private ICommand _navigateToSettingsPageCommand;

        public ICommand NavigateToSettingsPageCommand
            =>
            _navigateToSettingsPageCommand ??
            (_navigateToSettingsPageCommand = new DelegateCommand(NavigateToSettingsPage));

        private void NavigateToSettingsPage()
        {
            NavigationService.Navigate("Settings.SettingsMain", null);
            EventFired = true;
        }

        #endregion NavigateToSettingsPageCommand

        #region NavigateToWhisperPageCommand

        private ICommand _navigateToWhisperPageCommand;

        public ICommand NavigateToWhisperPageCommand
            =>
            _navigateToWhisperPageCommand ??
            (_navigateToWhisperPageCommand = new DelegateCommand(NavigateToWhisperPage));

        protected virtual void NavigateToWhisperPage()
        {
            EventFired = true;
        }

        #endregion NavigateToWhisperPageCommand

        #region NavigateToAccountPageCommand

        private ICommand _navigateToAccountPageCommand;

        public ICommand NavigateToAccountPageCommand
            =>
            _navigateToAccountPageCommand ??
            (_navigateToAccountPageCommand = new DelegateCommand(NavigateToAccountPage));

        private void NavigateToAccountPage()
        {
            NavigationService.Navigate("Pages.Account", null);
            EventFired = true;
        }

        #endregion NavigateToAccountPageCommand

        #endregion Commands
    }
}