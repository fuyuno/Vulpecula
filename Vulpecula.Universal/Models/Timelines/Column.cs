using System.Collections.Generic;
using System.Linq;

using Vulpecula.Models;

// ReSharper disable PossibleMultipleEnumeration

namespace Vulpecula.Universal.Models.Timelines
{
    public class Column
    {
        public ColumnInfo Info { get; set; }

        public User User { get; set; }

        public Column(ColumnInfo info, User user)
        {
            this.Info = info;
            this.User = user;
        }

        public static Column RelateUserToColumn(IEnumerable<User> users, ColumnInfo column)
        {
            if (users.Any(w => w.Id == column.UserId))
                return new Column(column, users.Single(w => w.Id == column.UserId));
            throw new KeyNotFoundException($"UserId:{column.UserId} is not found in users that already loading.");
        }
    }
}