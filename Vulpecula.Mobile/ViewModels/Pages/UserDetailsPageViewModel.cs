using System.Linq;

using Prism.Commands;
using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.Views.Popups;
using Vulpecula.Models;

namespace Vulpecula.Mobile.ViewModels.Pages
{
    public class UserDetailsPageViewModel : TabbedViewModel
    {
        private readonly User _user;

        public UserDetailsPageViewModel(ILocalization localization, INavigationService navigationService, AccountManager accountManager, User user)
            : base(localization, navigationService)
        {
            this._user = user;
            Title = accountManager.Users.First() == this._user ? this.GetLocalizedString("Me") : this.ScreenName;
            Icon = "user";
            NavigationTitle = accountManager.Users.First() == this._user ? this.GetLocalizedString("MePage") : this.ScreenName;

            this._user = user;
        }

        #region Properties

        public string Cover => this._user.CoverImageUrlHttps;

        public string UserIcon => this._user.ProfileImageUrlHttps;

        public string Username => this._user.Name;

        public string ScreenName => $"@{this._user.Name}";

        public string Bio => this._user.Description;

        public string Location => this._user.Location;

        public string Url => this._user.Url;

        public string Followings => $"{this._user.FriendsCount:N0}";

        public string Followers => $"{this._user.FollowersCount:N0}";

        public string Favorites => $"{this._user.FavoritesCount:N0}";

        public string Whispers => $"{this._user.StatusesCount:N0}";

        #region Image does not fit width of screen that saving scaling.
        #region Height
        private double _height;
        public double Height {
            get{return this._height;}
            set{this.SetProperty(ref this._height, value);}
        }
        #endregion

        #region Width
        private double _width;
        public double Width{
            get{return this._width;}
            set{
                if (this.SetProperty(ref this._width, value))
                {
                    var a = this._width * 200 / 550;
                    this.Height = this._width * 200 / 550;
                }
            }
        }
        #endregion
        #endregion

        #endregion

        #region Commands

        #region NavigateCommand

        private DelegateCommand _navigateCommand;

        public DelegateCommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand(Navigate));

        private void Navigate()
        {
            NavigationService.Navigate<StatusPage>();
        }

        #endregion

        #endregion
    }
}