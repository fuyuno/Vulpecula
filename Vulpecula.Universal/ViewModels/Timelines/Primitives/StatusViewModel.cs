using System;
using System.Windows.Input;

using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;

using Prism.Commands;
using Prism.Windows.Navigation;

using Vulpecula.Models;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Timelines.Primitive;
using Vulpecula.Universal.Services;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Timelines.Primitives
{
    public class StatusViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly CroudiaAccount _provider;

        public StatusModel Model { get; }

        public bool IsShare { get; }
        public bool IsComment { get; }
        public bool IsDirectMessage { get; }
        public bool HasImage { get; }

        public string CreatedAt => Model.CreatedAt.ToString("HH:mm");

        public string Via => Model.Source == null ? "" : $"via {Model.Source.Name}";

        public StatusViewModel(StatusModel statusModel, CroudiaAccount provider, INavigationService navigationService)
        {
            Model = statusModel;
            _provider = provider;
            _navigationService = navigationService;

            if (Model.IsDirectMessage)
            {
                IsDirectMessage = Model.IsDirectMessage;
                return;
            }

            IsShare = Model.SpreadStatus != null;
            IsComment = Model.QuotedStatus != null;
            HasImage = IsShare ? Model.SpreadStatus?.Entities?.Media != null : Model.Entities?.Media != null;
            IsFlyoutOpened = false;
        }

        public void Initialize() {}

        public void OnTappedOpenUserProfile(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate("Pages.UserDetail", Model.User);
        }

        private UserViewModel CreateUserViewModel(User user)
        {
            var uvm = new UserViewModel(user);
            CompositeDisposable.Add(uvm);
            return uvm;
        }

        #region Text

        private string _text;
        public string Text => _text ?? (_text = IsShare ? Model.SpreadStatus.Text.Trim() : Model.Text.Trim());

        #endregion Text

        #region Image

        private string _imageUrl;

        public string ImageUrl
            =>
            _imageUrl ??
            (_imageUrl =
            HasImage
            ? IsShare ? Model.SpreadStatus.Entities.Media.MediaUrlHttps : Model.Entities.Media.MediaUrlHttps
            : "");

        #endregion Image

        #region User

        private UserViewModel _user;

        public UserViewModel User
            => _user ?? (_user = CreateUserViewModel(IsShare ? Model.SpreadStatus.User : Model.User));

        #endregion User

        #region Recipient

        private UserViewModel _recipient;
        public UserViewModel Recipient => _recipient ?? (_recipient = CreateUserViewModel(Model.Recipient));

        #endregion Recipient

        #region ShareUser

        private UserViewModel _shareUser;
        public UserViewModel ShareUser => _shareUser ?? (_shareUser = CreateUserViewModel(Model.User));

        #endregion ShareUser

        #region IsFavorited

        private bool _isFavorited;

        public bool IsFavorited
        {
            get { return _isFavorited; }
            set { SetProperty(ref _isFavorited, value); }
        }

        #endregion IsFavorited

        #region IsShared

        private bool _isShared;

        public bool IsShared
        {
            get { return _isShared; }
            set { SetProperty(ref _isShared, value); }
        }

        #endregion IsShared

        #region IsCommented

        private bool _isCommented;

        public bool IsCommented
        {
            get { return _isCommented; }
            set { SetProperty(ref _isCommented, value); }
        }

        #endregion IsCommented

        #region IsFlyoutOpened

        private bool _isFlyoutOpened;

        public bool IsFlyoutOpened
        {
            get { return _isFlyoutOpened; }
            set { SetProperty(ref _isFlyoutOpened, value); }
        }

        #endregion IsFlyoutOpened

        #region SpreadCommand

        private ICommand _spreadCommand;

        public ICommand SpreadCommand => _spreadCommand ?? (_spreadCommand = new DelegateCommand(Spread, CanSpread));

        private void Spread()
        {
            ServiceProvider.RegisterService(new SpreadService(_provider, Model.Id, IsShared));
            IsShared = !IsShared;
        }

        private bool CanSpread()
        {
            return !IsDirectMessage && !User.IsProtected;
        }

        #endregion SpreadCommand

        #region FavoriteCommand

        private ICommand _favoriteCommand;

        public ICommand FavoriteCommand => _favoriteCommand ?? (_favoriteCommand = new DelegateCommand(Favorite, CanFavorite));

        private void Favorite()
        {
            ServiceProvider.RegisterService(new FavoriteService(_provider, Model.Id, IsFavorited));
            IsFavorited = !IsFavorited;
        }

        private bool CanFavorite()
        {
            return !IsDirectMessage;
        }

        #endregion FavoriteCommand

        #region ShareCommand

        private ICommand _shareCommand;

        public ICommand ShareCommand => _shareCommand ?? (_shareCommand = new DelegateCommand(Share, CanShare));

        private void Share()
        {
            // なんか違う気がする
            var dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += Callback;
            DataTransferManager.ShowShareUI();
        }

        private bool CanShare()
        {
            return !IsDirectMessage && !User.IsProtected;
        }

        private void Callback(DataTransferManager sender, DataRequestedEventArgs e)
        {
            var request = e.Request;
            request.Data.Properties.Title = " ";
            request.Data.SetText($"{Model.Text}{Environment.NewLine}https://croudia.com/voices/show/{Model.Id}");
            sender.DataRequested -= Callback;
        }

        #endregion ShareCommand

        #region QuoteCommand

        private ICommand _quoteCommand;
        public ICommand QuoteCommand => _quoteCommand ?? (_quoteCommand = new DelegateCommand(Quote, CanQuote));

        private void Quote()
        {
            //
        }

        private bool CanQuote()
        {
            return !IsDirectMessage && !User.IsProtected;
        }

        #endregion

        #region CommentCommand

        private ICommand _commentCommand;
        public ICommand CommentCommand => _commentCommand ?? (_commentCommand = new DelegateCommand(Comment, CanComment));

        private void Comment() {}

        private bool CanComment()
        {
            return !IsDirectMessage && !User.IsProtected;
        }

        #endregion

        #region DeleteCommand

        private ICommand _deleteCommand;

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(Delete, CanDelete));

        private void Delete()
        {
            ServiceProvider.RegisterService(new StatusDeleteService(_provider, Model.Id, IsDirectMessage));
        }

        private bool CanDelete()
        {
            return User.IsOwn;
        }

        #endregion DeleteCommand

        #region IsExpanded

        private bool _isExpaned;

        public bool IsExpanded
        {
            get { return _isExpaned; }
            set { SetProperty(ref _isExpaned, value); }
        }

        #endregion IsExpanded
    }
}