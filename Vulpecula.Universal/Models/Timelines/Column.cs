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

        public static IEnumerable<Column> RelateUserToColumn(IEnumerable<User> users, IEnumerable<ColumnInfo> columns)
        {
            var list = new List<Column>();
            foreach (var column in columns)
            {
                if (users.Any(w => w.Id == column.UserId))
                    list.Add(new Column(column, users.Single(w => w.Id == column.UserId)));
            }
            return list;
        }
    }
}