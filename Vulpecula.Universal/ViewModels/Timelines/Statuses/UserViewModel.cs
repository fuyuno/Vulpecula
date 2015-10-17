using System;

using Prism.Windows.Mvvm;

using Vulpecula.Models;

namespace Vulpecula.Universal.ViewModels.Timelines.Statuses
{
    public class UserViewModel : ViewModelBase
    {
        private readonly User _user;

        public string Name => this._user.Name;

        public string ScreenName => this._user.ScreenName;

        public string ProfileImageUrl => this._user.ProfileImageUrlHttps;

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