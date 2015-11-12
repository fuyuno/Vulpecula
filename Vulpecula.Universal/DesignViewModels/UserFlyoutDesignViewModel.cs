using System;

using JetBrains.Annotations;

using Vulpecula.Models;

namespace Vulpecula.Universal.DesignViewModels
{
    [UsedImplicitly]
    public class UserFlyoutDesignViewModel
    {
        private readonly User _user;

        public UserFlyoutDesignViewModel()
        {
            _user = new User
            {
                Id = 43391,
                IdStr = "43391",
                Name = "三日月 ふゆの",
                ScreenName = "MikazukiFuyuno",
                ProfileImageUrlHttps = "https://img.croudia.com/profile_images/MikazukiFuyuno/pzqDtyUUiiBeX7nyvcngxhwi.png",
                CoverImageUrlHttps = "https://img.croudia.com/profile_banners/MikazukiFuyuno/c1eBtKT53EYKeF8YPyOCTkgi.jpeg",
                CreatedAt = new DateTime(2015, 2, 15, 3, 1, 53, DateTimeKind.Local),
                Description = "三日月冬乃といいます。",
                FavoritesCount = 10,
                IsFollowRequestSent = false,
                FollowersCount = 15,
                IsFollowing = false,
                FriendsCount = 2,
                Location = string.Empty,
                StatusesCount = 67,
                IsProtected = false,
                Url = "https://about.mkzk.xyz"
            };
        }

        #region Properties

        public string ScreenName => $"@{this._user.ScreenName}";

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