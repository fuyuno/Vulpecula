using System;
using System.Linq;
using System.Windows.Input;

using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

using Vulpecula.Mobile.Extensions;
using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.Views.Pages;
using Vulpecula.Mobile.Views.Popups;
using Vulpecula.Models;

using Xamarin.Forms;

namespace Vulpecula.Mobile.ViewModels.Pages
{
    public class StatusDetailsPageViewModel : TabbedViewModel
    {
        private readonly AccountManager _accountManager;
        private readonly IPageDialogService _dialogService;
        private Status _status;

        public StatusDetailsPageViewModel(ILocalization localization, INavigationService navigationService, IPageDialogService dialogService, AccountManager accountManager)
            : base(localization, navigationService)
        {
            this._dialogService = dialogService;
            this._accountManager = accountManager;
            this.Title = this.GetLocalizedString("Details");
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            // Navigated from View
            if (parameters.ContainsKey("status"))
            {
                this._status = parameters["status"] as Status;
            }
            else
            {
                // Error Dialog
                await this._dialogService.DisplayAlert(
                    this.GetLocalizedString("Error"), this.GetLocalizedString("InvalidStatusError"), this.GetLocalizedString("OK"));
                return;
            }

            this.ScreenName = $"@{this._status.User.ScreenName}";
            this.UserName = this._status.User.Name.ToSingleLine();
            this.Text = this._status.Text.Trim();
            this.UserIcon = this._status.User.ProfileImageUrlHttps;
            this.CreatedAt = this._status.CreatedAt.ToString("G");
            this.Via = this._status.Source.Name.ToSingleLine();
            this.FavoritedCount = this._status.FavoritedCount;
            this.SpreadCount = this._status.SpreadCount;

            this.OnTappedShareCommand.ChangeCanExecute();

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

        #region Commands

        #region OnTappedShowUserDetailsCommand

        private ICommand _onTappedShowUserDetailsCommand;

        public ICommand OnTappedShowUserDetailsCommand =>
            _onTappedShowUserDetailsCommand ?? (_onTappedShowUserDetailsCommand = new Command(OnTappedShowUserDetails));

        private void OnTappedShowUserDetails()
        {
            var param = new NavigationParameters();
            param.Add("user", this._status.User);
            this.NavigationService.Navigate<UserDetailsPage>(param, false);
        }

        #endregion

        #region OnTappedOpenViaAppCommand

        private ICommand _onTappedOpenViaAppCommand;

        public ICommand OnTappedOpenViaAppCommand =>
            _onTappedOpenViaAppCommand ?? (_onTappedOpenViaAppCommand = new Command(OnTappedOpenViaApp));

        private void OnTappedOpenViaApp()
        {
            // TODO: Move to another class(Browser.cs ?)
            if (string.IsNullOrWhiteSpace(this._status.Source.Url))
            {
                return;
            }
            Device.OpenUri(new Uri(this._status.Source.Url));
        }

        #endregion

        #region OnTappedReplyCommand

        private ICommand _onTappedReplyCommand;
        public ICommand OnTappedReplyCommand => _onTappedReplyCommand ?? (_onTappedReplyCommand = new Command(OnTappedReply));

        private void OnTappedReply()
        {
            var param = new NavigationParameters();
            param.Add("status", $"{this.ScreenName} ");
            param.Add("in_reply_to_status_id", this._status.Id);
            this.NavigationService.Navigate<StatusPage>(param);
        }

        #endregion

        #region OnTappedShareCommand

        private Command _onTappedShareCommand;
        public Command OnTappedShareCommand => _onTappedShareCommand ?? (_onTappedShareCommand = new Command(OnTappedShare, CanOnTappedShare));

        private async void OnTappedShare()
        {
            await this._accountManager.Providers.First().Croudia.Statuses.SpreadAsync(this._status.Id);
        }

        private bool CanOnTappedShare()
        {
            if (this._status == null)
            {
                return true;
            }
            return !this._status.User.IsProtected;
        }

        #endregion

        #region OnTappedFavoriteCommand

        private ICommand _onTappedFavoriteCommand;
        public ICommand OnTappedFavoriteCommand => _onTappedFavoriteCommand ?? (_onTappedFavoriteCommand = new Command(OnTappedFavorite));

        private async void OnTappedFavorite()
        {
            await this._accountManager.Providers.First().Croudia.Favorites.CreateAsync(this._status.Id);
        }

        #endregion

        #region OnTappedCommentCommand

        private ICommand _onTappedCommentCommand;
        public ICommand OnTappedCommentCommand => _onTappedCommentCommand ?? (_onTappedCommentCommand = new Command(OnTappedComment));

        private void OnTappedComment()
        {
            // comment
        }

        #endregion

        #region OnTappedMoreCommand

        private ICommand _onTappedMoreCommand;
        public ICommand OnTappedMoreCommand => _onTappedMoreCommand ?? (_onTappedMoreCommand = new Command(OnTappedMore));

        private void OnTappedMore()
        {
            // more
        }

        #endregion

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