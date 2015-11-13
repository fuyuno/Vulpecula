using System;

using Vulpecula.Models;

namespace Vulpecula.Universal.DesignViewModels
{
    public class UserDesignViewModel
    {
        private readonly User _user;

        public string Name => this._user.Name.Replace(Environment.NewLine, "");

        public string ScreenName => $"@{this._user.ScreenName}";

        public string Icon => this._user.ProfileImageUrlHttps;

        public string CoverImageUrl => this._user.CoverImageUrlHttps;

        public DateTime CreatedAt => this._user.CreatedAt;

        public string Description => this._user.Description;

        public string Location => this._user.Location.Replace(Environment.NewLine, "");

        public bool IsProtected => this._user.IsProtected;

        public string Url => this._user.Url;

        public UserDesignViewModel()
        {
            this._user = new User
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
    }
}