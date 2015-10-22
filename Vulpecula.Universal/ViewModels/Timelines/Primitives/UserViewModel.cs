using System;

using Vulpecula.Models;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Timelines.Primitives
{
    public class UserViewModel : ViewModel
    {
        private readonly User _user;

        public string Name => this._user.Name;

        public string ScreenName => this._user.ScreenName;

        public string Icon => this._user.ProfileImageUrlHttps;

        public string CoverImageUrl => this._user.CoverImageUrlHttps;

        public DateTime CreatedAt => this._user.CreatedAt;

        public string Description => this._user.Description;

        public string Location => this._user.Location;

        public bool IsProtected => this._user.IsProtected;

        public string Url => this._user.Url;

        public UserViewModel(User user)
        {
            this._user = user;
        }
    }
}