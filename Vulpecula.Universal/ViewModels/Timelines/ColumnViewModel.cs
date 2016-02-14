using System.Collections.Generic;
using System.Linq;

using Prism.Windows.Navigation;

using Vulpecula.Models;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.ViewModels.Primitives;

// ReSharper disable PossibleMultipleEnumeration

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class ColumnViewModel : ViewModel
    {
        private readonly User _user;

        public Column Column { get; }

        public string Icon => _user.ProfileImageUrlHttps;
        public TimelineViewModel TimelineViewModel { get; }

        private ColumnViewModel(Column column, CroudiaProvider provider, INavigationService navigationService)
        {
            Column = column;
            TimelineViewModel = new TimelineViewModel(column, provider, navigationService);
            _user = provider.User;
        }

        public static ColumnViewModel Create(Column column, INavigationService navigationService)
        {
            if (AccountManager.Instance.Providers.All(w => w.User.Id != column.UserId))
                throw new KeyNotFoundException($"UserId:{column.UserId} is not found in users that loading.");
            return new ColumnViewModel(column, AccountManager.Instance.Providers.Single(w => w.User.Id == column.UserId), navigationService);
        }
    }
}