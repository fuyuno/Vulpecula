using Prism.Windows.Navigation;

using Vulpecula.Models;
using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.ViewModels.Primitives;

// ReSharper disable PossibleMultipleEnumeration

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class ColumnViewModel : ViewModel
    {
        private readonly User _user;

        public ColumnViewModel(Column column, INavigationService navigationService)
        {
            Column = column;
            TimelineViewModel = new TimelineViewModel(column, column.Account, navigationService);
            _user = column.Account.User;
        }

        public Column Column { get; }

        public string Icon => _user.ProfileImageUrlHttps;
        public TimelineViewModel TimelineViewModel { get; }
    }
}