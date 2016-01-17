using System.Collections.ObjectModel;
using System.Linq;
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
            Text = string.Empty;
            IsWhisperZoneOpened = false;
            EventFired = false;
            ViewModelHelper.SubscribeNotifyCollectionChanged(AccountManager.Instance.Users, Accounts, (User w) => UserAccountViewModel.Create(w));
        }

        #region Properties

        public ObservableCollection<UserAccountViewModel> Accounts { get; }

        #region Text Property

        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                if (SetProperty(ref _text, value))
                    ((DelegateCommand) SendTweetCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region IsWhisperZoneOpened

        private bool _isWhisperZoneOpened;

        public bool IsWhisperZoneOpened
        {
            get { return _isWhisperZoneOpened; }
            set { SetProperty(ref _isWhisperZoneOpened, value); }
        }

        #endregion

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

        #region ToggleTweetAreaCommand

        private ICommand _toggleTweetAreaCommand;
        public ICommand ToggleTweetAreaCommand => _toggleTweetAreaCommand ?? (_toggleTweetAreaCommand = new DelegateCommand(ToggleTweetArea));

        private void ToggleTweetArea()
        {
            IsWhisperZoneOpened = !IsWhisperZoneOpened;
        }

        #endregion

        #region SendTweetCommand

        private ICommand _sendTweetCommand;
        public ICommand SendTweetCommand => _sendTweetCommand ?? (_sendTweetCommand = new DelegateCommand(SendTweet, CanSendTweet));

        private void SendTweet()
        {
            foreach (var account in Accounts.Where(w => w.IsWhisperEnabled))
                account.SendWhisper(Text);
            Text = "";
            ((DelegateCommand) SendTweetCommand).RaiseCanExecuteChanged();
        }

        private bool CanSendTweet()
        {
            if (Accounts.All(w => !w.IsWhisperEnabled) || string.IsNullOrWhiteSpace(Text))
                return false;
            return true;
        }

        #endregion

        #region SelectAccountCommand

        private ICommand _selectAccountCommand;
        public ICommand SelectAccountCommand => _selectAccountCommand ?? (_selectAccountCommand = new DelegateCommand(SelectAccount));

        private void SelectAccount()
        {
            ((DelegateCommand) SendTweetCommand).RaiseCanExecuteChanged();
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