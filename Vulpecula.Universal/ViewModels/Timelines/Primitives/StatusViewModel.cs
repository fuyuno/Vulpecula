using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Commands;
using Prism.Windows.Navigation;
using Vulpecula.Models;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Services;
using Vulpecula.Universal.Models.Timelines.Primitive;
using Vulpecula.Universal.ViewModels.Flyouts;
using Vulpecula.Universal.ViewModels.Primitives;
using Vulpecula.Universal.Views.Timelines.Primitive;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Vulpecula.Universal.ViewModels.Timelines.Primitives
{
    public class StatusViewModel : ViewModel
    {
        private readonly CroudiaProvider _provider;
        public StatusModel Model { get; }

        public bool IsShare { get; }
        public bool IsComment { get; }
        public bool IsDirectMessage { get; }
        public bool HasImage { get; }

        public string CreatedAt => Model.CreatedAt.ToString("HH:mm");

        public string Via => Model.Source == null ? "" : $"via {Model.Source.Name}";

        public StatusViewModel(StatusModel statusModel, CroudiaProvider provider)
        {
            Model = statusModel;
            _provider = provider;

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

        public void Initialize()
        {
            DataTransferManager.GetForCurrentView().DataRequested += (sender, args) =>
            {
                // Share via (args.request...)
            };
        }

        public void OnTappedOpenUserProfile(object sender, RoutedEventArgs e)
        {
            // TODO: ヤバイので、Behavior でなんとかする。
            var uc = (StatusView)((Grid)((StackPanel)((Grid)((Image)sender).Parent).Parent).Parent).Parent;
            var flyout = uc.FindName("Flyout") as SettingsFlyout;
            if (flyout != null)
            {
                flyout.DataContext = ((Grid)((Image)sender).Parent).Children[0] == (Image)sender ? UserProfile : CreateUserFlyoutViewModel(this.Model.Recipient);
                flyout.ShowIndependent();
            }
        }

        private UserViewModel CreateUserViewModel(User user)
        {
            var uvm = new UserViewModel(user);
            CompositeDisposable.Add(uvm);
            return uvm;
        }

        private UserFlyoutViewModel CreateUserFlyoutViewModel(User user)
        {
            var ufvm = new UserFlyoutViewModel(user);
            CompositeDisposable.Add(ufvm);
            return ufvm;
        }

        #region Text

        private string _text;
        public string Text => _text ?? (_text = IsShare ? Model.SpreadStatus.Text.Trim() : Model.Text.Trim());

        #endregion Text

        #region Image

        private string _imageUrl;
        public string ImageUrl => _imageUrl ?? (_imageUrl = HasImage ? IsShare ? Model.SpreadStatus.Entities.Media.MediaUrlHttps : Model.Entities.Media.MediaUrlHttps : "");

        #endregion Image

        #region User

        private UserViewModel _user;
        public UserViewModel User => _user ?? (_user = CreateUserViewModel(IsShare ? Model.SpreadStatus.User : Model.User));

        #endregion User

        #region UserProfile

        private UserFlyoutViewModel _userProfile;
        public UserFlyoutViewModel UserProfile => _userProfile ?? (_userProfile = CreateUserFlyoutViewModel(IsShare ? Model.SpreadStatus.User : Model.User));

        #endregion UserProfile

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

        public ICommand SpreadCommand => _spreadCommand ?? (_spreadCommand = new DelegateCommand(Spread));

        private void Spread()
        {
            ServiceProvider.RegisterService(new SpreadService(_provider, Model.Id, IsShared));
            IsShared = !IsShared;
        }

        #endregion SpreadCommand

        #region FavoriteCommand

        private ICommand _favoriteCommand;

        public ICommand FavoriteCommand => _favoriteCommand ?? (_favoriteCommand = new DelegateCommand(Favorite));

        private void Favorite()
        {
            ServiceProvider.RegisterService(new FavoriteService(_provider, Model.Id, IsFavorited));
            IsFavorited = !IsFavorited;
        }

        #endregion FavoriteCommand

        #region DeleteCommand

        private ICommand _deleteCommand;

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(Delete));

        private void Delete()
        {
            ServiceProvider.RegisterService(new StatusDeleteService(_provider, Model.Id, IsDirectMessage));
        }

        #endregion DeleteCommand

        #region ShareCommand

        private ICommand _shareCommand;

        public ICommand ShareCommand => _shareCommand ?? (_shareCommand = new DelegateCommand(Share));

        private void Share()
        {
            DataTransferManager.ShowShareUI();
        }

        #endregion ShareCommand

        #region IsExpanded

        private bool _isExpaned;

        public bool IsExpanded
        {
            get { return this._isExpaned; }
            set { this.SetProperty(ref this._isExpaned, value); }
        }

        #endregion IsExpanded
    }
}