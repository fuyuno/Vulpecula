using System.Collections.ObjectModel;
using System.Linq;

using Prism.Commands;
using Prism.Windows.Navigation;

using Vulpecula.Universal.Models;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Pages
{
    public class TweetPageViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;

        public TweetPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Accounts = AccountManager.Instance.Users.Select(UserAccountViewModel.Create)
                                     .ToList()
                                     .AsReadOnly();
            SelectedAccount = Accounts.First();
            WhisperCount = 372;
            WhisperText = string.Empty;
        }

        #region Properties

        public ReadOnlyCollection<UserAccountViewModel> Accounts { get; }

        #region SelectedAccount

        // TODO: object to UserAccountViewModel converter
        private object _selectedAccount;

        public object SelectedAccount
        {
            get { return _selectedAccount; }
            set { SetProperty(ref _selectedAccount, value); }
        }

        #endregion SelectedAccount

        #region WhisperText

        private string _whisperText;

        public string WhisperText
        {
            get { return _whisperText; }
            set
            {
                if (SetProperty(ref _whisperText, value))
                {
                    WhisperCount = 372 - _whisperText.Length;
                    SendTweetCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion WhisperText

        #region WhisperCount

        private int _whisperCount;

        public int WhisperCount
        {
            get { return _whisperCount; }
            set { SetProperty(ref _whisperCount, value); }
        }

        #endregion WhisperCount

        #endregion Properties

        #region Commands

        #region SendTweetCommand

        private DelegateCommand _sendTweetCommand;

        public DelegateCommand SendTweetCommand => _sendTweetCommand ??
                                                   (_sendTweetCommand = new DelegateCommand(SendTweet, CanSendTweet));

        private void SendTweet()
        {
            (SelectedAccount as UserAccountViewModel)?.SendWhisper(WhisperText);
            _navigationService.GoBack();
        }

        private bool CanSendTweet()
        {
            if (WhisperText.Length == 0 || WhisperText.Length > 372)
                return false;
            return true;
        }

        #endregion SendTweetCommand

        #endregion Commands
    }
}