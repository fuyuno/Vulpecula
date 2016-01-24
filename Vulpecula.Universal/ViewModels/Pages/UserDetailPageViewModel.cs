using System.Collections.Generic;

using Prism.Windows.Navigation;

using Vulpecula.Models;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Pages
{
    public class UserDetailPageViewModel : ViewModel
    {
        private User _user;

        #region Overrides of ViewModelBase

        /// <summary>
        /// Called when navigation is performed to a page. You can use this method to load state if it is available.
        /// </summary>
        /// <param name="e">The <see cref="T:Prism.Windows.Navigation.NavigatedToEventArgs"/> instance containing the event data.</param><param name="viewModelState">The state of the view model.</param>
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            _user = ((User) e.Parameter);
        }

        #endregion

        #region Properties

        public string ScreenName => $"@{_user.ScreenName}";

        public string UserName => _user.Name;

        public string IconUrl => _user.ProfileImageUrlHttps.EndsWith("default.png") ? "ms-appx:///Assets/Icon.png" : _user.ProfileImageUrlHttps;

        public string CoverUrl => _user.CoverImageUrlHttps.EndsWith("default.png") ? "ms-appx:///Assets/Header.png" : _user.CoverImageUrlHttps;

        public string Bio => _user.Description;

        public string Location => _user.Location;

        public string Followings => $"{_user.FriendsCount:N0}";

        public string Followers => $"{_user.FollowersCount:N0}";

        public string Favorites => $"{_user.FavoritesCount:N0}";

        public string Whispers => $"{_user.StatusesCount:N0}";

        #endregion
    }
}