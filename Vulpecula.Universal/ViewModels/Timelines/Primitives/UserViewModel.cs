using System;
using System.Linq;

using Vulpecula.Models;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Timelines.Primitives
{
    public class UserViewModel : ViewModel
    {
        private readonly User _user;

        public string Name => _user.Name.Replace(Environment.NewLine, "");

        public string ScreenName => $"@{_user.ScreenName}";

        public string Icon => _user.ProfileImageUrlHttps;

        public string CoverImageUrl => _user.CoverImageUrlHttps;

        public DateTime CreatedAt => _user.CreatedAt;

        public string Description => _user.Description;

        public string Location => _user.Location.Replace(Environment.NewLine, "");

        public bool IsProtected => _user.IsProtected;

        public string Url => _user.Url;

        public bool IsOwn => AccountManager.Instance.Users.Any(w => w.Id == _user.Id);

        public UserViewModel(User user)
        {
            _user = user;
        }
    }
}