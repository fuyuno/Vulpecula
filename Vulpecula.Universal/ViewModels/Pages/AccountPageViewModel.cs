using System.Collections.ObjectModel;
using System.Linq;

using JetBrains.Annotations;

using Vulpecula.Universal.Models;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Pages
{
    [UsedImplicitly]
    public class AccountPageViewModel : ViewModel
    {
        public AccountPageViewModel()
        {
            Accounts = AccountManager.Instance.Users.Select(UserAccountViewModel.Create)
                                     .ToList()
                                     .AsReadOnly();
        }

        // TODO: ReadOnlyCollection To ReadOnlyObservableCollection
        public ReadOnlyCollection<UserAccountViewModel> Accounts { get; }
    }
}