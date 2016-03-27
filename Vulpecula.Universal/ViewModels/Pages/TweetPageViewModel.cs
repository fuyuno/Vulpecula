using System.Collections.ObjectModel;
using System.Linq;

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
        }

        #region Properties

        public ReadOnlyCollection<UserAccountViewModel> Accounts { get; }

        #region SelectedAccount

        private UserAccountViewModel _selectedAccount;

        public UserAccountViewModel SelectedAccount
        {
            get { return _selectedAccount; }
            set { SetProperty(ref _selectedAccount, value); }
        }

        #endregion SelectedAccount

        #endregion Properties
    }
}