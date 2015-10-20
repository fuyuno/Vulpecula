using System.Collections.Generic;
using System.Linq;

using Vulpecula.Models;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Timelines;

// ReSharper disable PossibleMultipleEnumeration

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class ColumnViewModel
    {
        private readonly Column _column;
        private readonly CroudiaProvider _provider;
        private readonly User _user;

        public string Name => this._column.Name;

        public string Icon => this._user.ProfileImageUrlHttps;

        private ColumnViewModel(Column column, CroudiaProvider provider)
        {
            this._column = column;
            this._user = provider.User;
            this._provider = provider;
        }

        public static ColumnViewModel Create(IEnumerable<CroudiaProvider> providers, Column column)
        {
            if (providers.Any(w => w.User.Id == column.UserId))
            {
                var provider = providers.Single(w => w.User.Id == column.UserId);
                return new ColumnViewModel(column, provider);
            }
            throw new KeyNotFoundException($"UserId:{column.UserId} is not found in users that already loading.");
        }
    }
}