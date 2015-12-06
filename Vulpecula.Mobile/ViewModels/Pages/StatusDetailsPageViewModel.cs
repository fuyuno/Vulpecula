using System;

using Xamarin.Forms;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.Models.Interfaces;
using Prism.Navigation;
using Vulpecula.Models;

namespace Vulpecula.Mobile.ViewModels.Pages
{
    public class StatusDetailsPageViewModel : TabbedViewModel
    {
        private Status _status;

        public StatusDetailsPageViewModel(ILocalization localization, INavigationService navigationService)
            : base(localization, navigationService)
        {
            this.Title = this.GetLocalizedString("TweetDetailsPage");
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            // Navigated from View
            if (parameters.ContainsKey("status"))
            {
                this._status = parameters["status"] as Status;
            }
            else
            {
                // Error Dialog
                return;
            }

            this.ScreenName = $"@{this._status.User.ScreenName}";
            this.UserName = this._status.User.Name;
            this.Text = this._status.Text;
            this.UserIcon = this._status.User.ProfileImageUrlHttps;
            this.CreatedAt = this._status.CreatedAt.ToString("G");
            this.Via = this._status.Source.Name;
            this.FavoritedCount = this._status.FavoritedCount;

            base.OnNavigatedTo(parameters);
        }

        #region Properties

        #region ScreenName

        private string _screenName;

        public string ScreenName
        {
            get { return this._screenName; }
            set { this.SetProperty(ref this._screenName, value); }
        }

        #endregion

        #region UserName

        private string _userName;

        public string UserName
        {
            get { return this._userName; }
            set { this.SetProperty(ref this._userName, value); }
        }

        #endregion

        #region Text

        private string _text;

        public string Text
        {
            get { return this._text; }
            set { this.SetProperty(ref this._text, value); }
        }

        #endregion

        #region Icon

        private string _userIcon;

        public string UserIcon
        {
            get { return this._userIcon; }
            set { this.SetProperty(ref this._userIcon, value); }
        }

        #endregion

        #region CreatedAt

        private string _createdAt;

        public string CreatedAt
        {
            get { return this._createdAt; }
            set { this.SetProperty(ref this._createdAt, value); }
        }

        #endregion

        #region Via

        private string _via;

        public string Via
        {
            get { return this._via; }
            set { this.SetProperty(ref this._via, value); }
        }

        #endregion

        #region FavoritedCount

        private long _favoritedCount;

        public long FavoritedCount
        {
            get { return this._favoritedCount; }
            set { this.SetProperty(ref this._favoritedCount, value); }
        }

        #endregion

        #region SpreadCount

        private long _spreadCount;

        public long SpreadCount
        {
            get { return this._spreadCount; }
            set { this.SetProperty(ref this._spreadCount, value); }
        }

        #endregion

        #endregion

    }
}


