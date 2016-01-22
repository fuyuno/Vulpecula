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
        private readonly IPageDialogService _dialogService;

        // aa~^
        public Status Model { get; private set; }

        public AccountManager AccountManager { get; }

        public new ILocalization Localization
        {
            get { return base.Localization; }
        }

        public new INavigationService NavigationService
        {
            get { return base.NavigationService; }
        }

        public StatusDetailsPageViewModel(ILocalization localization, INavigationService navigationService, IPageDialogService dialogService, AccountManager accountManager)
        : base(localization, navigationService)
        {
            _dialogService = dialogService;
            AccountManager = accountManager;
            Title = GetLocalizedString("Details");
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            // Navigated from View
            if (parameters.ContainsKey("status"))
                Model = ((Status) parameters["status"]);
            else
            {
                // Error Dialog
                await _dialogService.DisplayAlert(GetLocalizedString("Error"), GetLocalizedString("InvalidStatusError"), GetLocalizedString("OK"));
                return;
            }

            ScreenName = $"@{Model.User.ScreenName}";
            UserName = Model.User.Name.ToSingleLine();
            Text = Model.Text.Trim();
            UserIcon = Model.User.ProfileImageUrlHttps;
            CreatedAt = Model.CreatedAt.ToString("G");
            Via = Model.Source.Name.ToSingleLine();
            FavoritedCount = Model.FavoritedCount;
            SpreadCount = Model.SpreadCount;
            HasImage = Model.Entities?.Media?.MediaUrlHttps != null;
            ImageUrl = HasImage ? Model.Entities.Media.MediaUrlHttps : "";

            OnTappedShareCommand.ChangeCanExecute();
            base.OnNavigatedTo(parameters);
        }

        #region Properties

        #region ScreenName

        private string _screenName;

        public string ScreenName
        {
            get { return _screenName; }
            set { SetProperty(ref _screenName, value); }
        }

        #endregion

        #region UserName

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        #endregion

        #region Text

        private string _text;

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        #endregion

        #region Icon

        private string _userIcon;

        public string UserIcon
        {
            get { return _userIcon; }
            set { SetProperty(ref _userIcon, value); }
        }

        #endregion

        #region CreatedAt

        private string _createdAt;

        public string CreatedAt
        {
            get { return _createdAt; }
            set { SetProperty(ref _createdAt, value); }
        }

        #endregion

        #region Via

        private string _via;

        public string Via
        {
            get { return _via; }
            set { SetProperty(ref _via, value); }
        }

        #endregion

        #region FavoritedCount

        private long _favoritedCount;

        public long FavoritedCount
        {
            get { return _favoritedCount; }
            set { SetProperty(ref _favoritedCount, value); }
        }

        #endregion

        #region SpreadCount

        private long _spreadCount;

        public long SpreadCount
        {
            get { return _spreadCount; }
            set { SetProperty(ref _spreadCount, value); }
        }

        #endregion

        #region HasImage

        private bool _hasImage;

        public bool HasImage
        {
            get { return _hasImage; }
            set { SetProperty(ref _hasImage, value); }
        }

        #endregion

        #region ImageUrl

        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
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
            var param = new NavigationParameters { ["user"] = Model.User };
            NavigationService.Navigate<UserDetailsPage>(param, false);
        }

        #endregion

        #region OnTappedOpenViaAppCommand

        private ICommand _onTappedOpenViaAppCommand;

        public ICommand OnTappedOpenViaAppCommand =>
        _onTappedOpenViaAppCommand ?? (_onTappedOpenViaAppCommand = new Command(OnTappedOpenViaApp));

        private void OnTappedOpenViaApp()
        {
            // TODO: Move to another class(Browser.cs ?)
            if (string.IsNullOrWhiteSpace(Model.Source.Url))
                return;
            Device.OpenUri(new Uri(Model.Source.Url));
        }

        #endregion

        #region OnTappedReplyCommand

        private ICommand _onTappedReplyCommand;
        public ICommand OnTappedReplyCommand => _onTappedReplyCommand ?? (_onTappedReplyCommand = new Command(OnTappedReply));

        private void OnTappedReply()
        {
            var param = new NavigationParameters
            {
                ["status"] = $"{ScreenName} ",
                ["in_reply_to_status_id"] = Model.Id
            };
            NavigationService.Navigate<StatusPage>(param);
        }

        #endregion

        #region OnTappedShareCommand

        private Command _onTappedShareCommand;
        public Command OnTappedShareCommand => _onTappedShareCommand ?? (_onTappedShareCommand = new Command(OnTappedShare, CanOnTappedShare));

        private async void OnTappedShare()
        {
            await AccountManager.Providers.First().Croudia.Statuses.SpreadAsync(Model.Id);
        }

        private bool CanOnTappedShare()
        {
            if (Model == null)
                return true;
            return !Model.User.IsProtected;
        }

        #endregion

        #region OnTappedFavoriteCommand

        private ICommand _onTappedFavoriteCommand;
        public ICommand OnTappedFavoriteCommand => _onTappedFavoriteCommand ?? (_onTappedFavoriteCommand = new Command(OnTappedFavorite));

        private async void OnTappedFavorite()
        {
            await AccountManager.Providers.First().Croudia.Favorites.CreateAsync(Model.Id);
        }

        #endregion

        #region OnTappedCommentCommand

        private ICommand _onTappedCommentCommand;
        public ICommand OnTappedCommentCommand => _onTappedCommentCommand ?? (_onTappedCommentCommand = new Command(OnTappedComment));

        private void OnTappedComment()
        {
            var parameters = new NavigationParameters { ["comment"] = Model.Id };
            NavigationService.Navigate<StatusPage>(parameters);
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
            var parameters = new NavigationParameters
            {
                ["status"] = ScreenName + " ",
                ["in_reply_to_status_id"] = Model.Id
            };
            NavigationService.Navigate<StatusPage>(parameters);
        }

        #endregion

        #endregion
    }
}