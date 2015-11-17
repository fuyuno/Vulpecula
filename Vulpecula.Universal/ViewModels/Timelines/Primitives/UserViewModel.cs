using System;
using System.Linq;

using Vulpecula.Models;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Timelines.Primitives
{
    public class UserViewModel : ViewModel
    {
        public User User { get; }

        public string Name => User.Name.Replace(Environment.NewLine, "");

        public string ScreenName => $"@{User.ScreenName}";

        public string Icon => User.ProfileImageUrlHttps;

        public string CoverImageUrl => User.CoverImageUrlHttps;

        public DateTime CreatedAt => User.CreatedAt;

        public string Description => User.Description;

        public string Location => User.Location.Replace(Environment.NewLine, "");

        public bool IsProtected => User.IsProtected;

        public string Url => User.Url;

        public bool IsOwn => AccountManager.Instance.Users.Any(w => w.Id == User.Id);

        public UserViewModel(User user)
        {
            User = user;
        }
    }
}