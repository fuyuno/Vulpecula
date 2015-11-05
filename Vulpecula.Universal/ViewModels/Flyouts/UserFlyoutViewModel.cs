using System.Linq;

using Vulpecula.Models;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.ViewModels.Primitives;

// ReSharper disable InconsistentNaming

namespace Vulpecula.Universal.ViewModels.Flyouts
{
    public class UserFlyoutViewModel : ViewModel
    {
        private readonly User _user;

        public UserFlyoutViewModel(User user)
        {
            _user = user;
            FollowType = FollowTypes.Loading;

            var me = AccountManager.Instance.Users.First();
            AccountManager.Instance.Providers.First().Croudia.FriendShips
                .ShowAsync(source_id => me.Id, target_id => user.Id)
                .ContinueWith(task =>
                {
                    var relation = task.Result;
                    if (AccountManager.Instance.Users.Any(w => w.Id == _user.Id))
                    {
                        FollowType = FollowTypes.Me;
                        return;
                    }

                    if (relation.RelationShip.Source.IsBlocking.HasValue && relation.RelationShip.Source.IsBlocking.Value)
                    {
                        FollowType = FollowTypes.Blocking;
                        return;
                    }

                    var b1 = _user.IsFollowRequestSent as bool?;
                    if (b1 != null && b1.Value)
                    {
                        FollowType = FollowTypes.Pending;
                        return;
                    }

                    var b2 = _user.IsFollowing as bool?;
                    if (b2 == null)
                    {
                        FollowType = FollowTypes.Unknown;
                        return;
                    }
                    FollowType = b2.Value ? FollowTypes.Following : FollowTypes.NoFollowing;
                });
        }

        #region Properties

        public string ScreenName => $"@{this._user.ScreenName}";

        public string UserName => _user.Name;

        public string IconUrl => _user.ProfileImageUrlHttps;

        public string CoverUrl => _user.CoverImageUrlHttps;

        public string Bio => _user.Description;

        public string Location => _user.Location;

        public long Followings => _user.FriendsCount;

        public long Followers => _user.FollowersCount;

        public long Favorites => _user.FavoritesCount;

        public long Whispers => _user.StatusesCount;

        #region FollowType

        private FollowTypes _followType;

        public FollowTypes FollowType
        {
            get { return _followType; }
            set { this.SetProperty(ref _followType, value); }
        }

        #endregion

        #endregion
    }
}