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
        private AccountManager _accountManager;

        public UserDetailsPageViewModel(ILocalization localization, INavigationService navigationService, AccountManager accountManager)
            : base(localization, navigationService)
        {
            this._accountManager= accountManager;
            Title = this.GetLocalizedString("Me");
            Icon = "user";
            NavigationTitle = this.GetLocalizedString("MePage");
        }
           
        // Tab
        public override void OnTabNavigatedTo()
        {
            this.Set(this._accountManager.Users.First());
        }

        // Modai
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            User user;
            // Navigated from View
            if (parameters.ContainsKey("user"))
            {
                user = parameters["user"] as User;
            } 
            else
            {
                user = this._accountManager.Users.First();
            }
            this.Set(user);
            this.Title = this.ScreenName;
            base.OnNavigatedTo(parameters);
        }

        private void Set(User user)
        {
            this.Cover = user.CoverImageUrlHttps;
            this.UserIcon = user.ProfileImageUrlHttps;
            this.Username = user.Name;
            this.ScreenName = $"@{user.ScreenName}";
            this.Bio = user.Description;
            this.Location = user.Location;
            this.Url = user.Url;
            this.Followings = $"{user.FriendsCount:N0}";
            this.Followers = $"{user.FollowersCount:N0}";
            this.Favorites = $"{user.FavoritesCount:N0}";
            this.Whispers = $"{user.StatusesCount:N0}";
        }

        #region Properties

        #region Cover

        private string _cover;

        public string Cover
        {
            get{ return this._cover; }
            set{ this.SetProperty(ref this._cover, value); }
        }

        #endregion

        #region UserIcon

        private string _userIcon;

        public string UserIcon
        {
            get{ return this._userIcon; }
            set{ this.SetProperty(ref this._userIcon, value); }
        }

        #endregion

        #region Username

        private string _username;

        public string Username
        {
            get{ return this._username; }
            set{ this.SetProperty(ref this._username, value); }
        }

        #endregion

        #region ScreenName

        private string _screenName;

        public string ScreenName
        {
            get{ return this._screenName; }
            set{ this.SetProperty(ref this._screenName, value); }
        }

        #endregion

        #region Bio

        private string _bio;

        public string Bio
        {
            get{ return this._bio; }
            set{ this.SetProperty(ref this._bio, value); }
        }

        #endregion

        #region Location

        private string _location;

        public string Location
        {
            get{ return this._location; }
            set{ this.SetProperty(ref this._location, value); }
        }

        #endregion

        #region Url

        private string _url;

        public string Url
        {
            get { return this._url; }
            set { this.SetProperty(ref this._url, value); }
        }

        #endregion

        #region

        private string _followings;

        public string Followings
        {
            get { return this._followings; }
            set { this.SetProperty(ref this._followings, value); }
        }

        #endregion

        #region Followers

        private string _followers;

        public string Followers
        {
            get { return this._followers; }
            set { this.SetProperty(ref this._followers, value); }
        }

        #endregion

        #region Favorites

        private string _favorites;

        public string Favorites
        {
            get { return this._favorites; }
            set { this.SetProperty(ref this._favorites, value); }
        }

        #endregion
       
        #region Whispers

        private string _whispers;

        public string Whispers
        {
            get{ return this._whispers; }
            set{ this.SetProperty(ref this._whispers, value); }
        }

        #endregion



        #region Image does not fit width of screen that saving scaling.

        #region Height

        private double _height;

        public double Height
        {
            get { return this._height; }
            set { this.SetProperty(ref this._height, value); }
        }

        #endregion

        #region Width

        private double _width;

        public double Width
        {
            get { return this._width; }
            set
            {
                if (this.SetProperty(ref this._width, value))
                {
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