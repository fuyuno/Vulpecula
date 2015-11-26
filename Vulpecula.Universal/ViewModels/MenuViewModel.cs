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
            this._navigationService = navigationService;
            this.Accounts = new ObservableCollection<UserAccountViewModel>();
            this.Text = string.Empty;
            this.IsWhisperZoneOpened = false;
            ViewModelHelper.SubscribeNotifyCollectionChanged(AccountManager.Instance.Users, this.Accounts, (User w) => UserAccountViewModel.Create(w));
        }

        #region Properties

        public ObservableCollection<UserAccountViewModel> Accounts { get; }

        #region Text Property

        private string _text;

        public string Text
        {
            get { return this._text; }
            set
            {
                if (this.SetProperty(ref this._text, value))
                {
                    ((DelegateCommand)this.SendTweetCommand).RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region IsWhisperZoneOpened

        private bool _isWhisperZoneOpened;

        public bool IsWhisperZoneOpened
        {
            get { return this._isWhisperZoneOpened; }
            set { this.SetProperty(ref this._isWhisperZoneOpened, value); }
        }

        #endregion

        #endregion

        #region Commands

        #region AuthorizationCommand

        private ICommand _authorizationCommand;

        public ICommand AuthorizationCommand => this._authorizationCommand ?? (this._authorizationCommand = new DelegateCommand(Authorization));

        private async void Authorization() => await AccountManager.Instance.AuthorizationAccount();

        #endregion

        #region ToggleTweetAreaCommand

        private ICommand _toggleTweetAreaCommand;
        public ICommand ToggleTweetAreaCommand => this._toggleTweetAreaCommand ?? (this._toggleTweetAreaCommand = new DelegateCommand(ToggleTweetArea));

        private void ToggleTweetArea()
        {
            this.IsWhisperZoneOpened = !this.IsWhisperZoneOpened;
        }

        #endregion

        #region SendTweetCommand

        private ICommand _sendTweetCommand;
        public ICommand SendTweetCommand => this._sendTweetCommand ?? (this._sendTweetCommand = new DelegateCommand(SendTweet, CanSendTweet));

        private void SendTweet()
        {
            foreach (var account in this.Accounts.Where(w => w.IsWhisperEnabled))
            {
                account.SendWhisper(this.Text);
            }
            this.Text = "";
            ((DelegateCommand)this.SendTweetCommand).RaiseCanExecuteChanged();
        }

        private bool CanSendTweet()
        {
            if (this.Accounts.All(w => !w.IsWhisperEnabled) || string.IsNullOrWhiteSpace(this.Text))
            {
                return false;
            }
            return true;
        }

        #endregion

        #region SelectAccountCommand

        private ICommand _selectAccountCommand;
        public ICommand SelectAccountCommand => this._selectAccountCommand ?? (this._selectAccountCommand = new DelegateCommand(SelectAccount));

        private void SelectAccount()
        {
            ((DelegateCommand)this.SendTweetCommand).RaiseCanExecuteChanged();
        }

        #endregion

        #region NavigateToSettingsPageCommand

        private ICommand _navigateToSettingsPageCommand;
        public ICommand NavigateToSettingsPageCommand => this._navigateToSettingsPageCommand ?? (this._navigateToSettingsPageCommand = new DelegateCommand(NavigateToSettingsPage));

        private void NavigateToSettingsPage()
        {
            this._navigationService.Navigate("Settings.SettingsGeneral", null);
        }

        #endregion

        #endregion
    }
}