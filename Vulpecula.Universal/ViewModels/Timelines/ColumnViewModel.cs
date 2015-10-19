using System.Collections.Generic;
using System.Linq;

using Vulpecula.Models;
using Vulpecula.Universal.Models.Timelines;

// ReSharper disable PossibleMultipleEnumeration

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class ColumnViewModel
    {
        private readonly Column _column;
        private readonly User _user;

        public string Name => this._column.Name;

        public string Icon => this._user.ProfileImageUrlHttps;

        private ColumnViewModel(Column column, User user)
        {
            this._column = column;
            this._user = user;
        }

        public static ColumnViewModel Create(IEnumerable<User> users, Column column)
        {
            if (users.Any(w => w.Id == column.UserId))
                return new ColumnViewModel(column, users.Single(w => w.Id == column.UserId));
            throw new KeyNotFoundException($"UserId:{column.UserId} is not found in users that already loading.");
        }
    }
}