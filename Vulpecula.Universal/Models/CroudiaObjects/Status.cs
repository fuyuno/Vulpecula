using System.Collections.ObjectModel;

using Vulpecula.Models;

namespace Vulpecula.Universal.Models.CroudiaObjects
{
    public class Status : Vulpecula.Models.Status
    {
        public ObservableCollection<User> SharedUsers { get; set; }

        public ObservableCollection<User> FavoritedUsers { get; set; }
    }
}