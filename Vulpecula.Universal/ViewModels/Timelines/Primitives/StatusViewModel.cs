using System.Diagnostics;

using Windows.UI.Xaml;

using Vulpecula.Models;
using Vulpecula.Universal.Models.Timelines.Primitive;
using Vulpecula.Universal.ViewModels.Flyouts;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Timelines.Primitives
{
    public class StatusViewModel : ViewModel
    {
        public StatusModel Model { get; }

        public bool IsShare { get; }
        public bool IsComment { get; }
        public bool IsDirectMessage { get; }
        public bool HasImage { get; }

        #region CreatedAt

        public string CreatedAt => Model.CreatedAt.ToString("HH:mm");

        #endregion

        public string Via => Model.Source == null ? "" : $"via {Model.Source.Name}";

        public StatusViewModel(StatusModel statusModel)
        {
            Model = statusModel;
            if (Model.IsDirectMessage)
            {
                IsDirectMessage = Model.IsDirectMessage;
                return;
            }

            IsShare = Model.SpreadStatus != null;
            IsComment = Model.QuotedStatus != null;
            HasImage = Model.Entities?.Media != null;
            IsFlyoutOpened = false;
        }

        public void OnTappedOpenUserProfile(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Called");
            IsFlyoutOpened = true;
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
        public string Text => _text ?? (_text = IsShare ? Model.SpreadStatus.Text : Model.Text);

        #endregion

        #region Image

        private string _imageUrl;
        public string ImageUrl => _imageUrl ?? (_imageUrl = HasImage ? Model.Entities.Media.MediaUrlHttps : "");

        #endregion

        #region User

        private UserViewModel _user;
        public UserViewModel User => _user ?? (_user = CreateUserViewModel(IsShare ? Model.SpreadStatus.User : Model.User));

        #endregion

        #region UserProfile

        private UserFlyoutViewModel _userProfile;
        public UserFlyoutViewModel UserProfile => _userProfile ?? (_userProfile = CreateUserFlyoutViewModel(IsShare ? Model.SpreadStatus.User : Model.User));

        #endregion

        #region Recipient

        private UserViewModel _recipient;
        public UserViewModel Recipient => _recipient ?? (_recipient = CreateUserViewModel(Model.Recipient));

        #endregion

        #region ShareUser

        private UserViewModel _shareUser;
        public UserViewModel ShareUser => _shareUser ?? (_shareUser = CreateUserViewModel(Model.User));

        #endregion

        #region IsFavorited

        private bool _isFavorited;

        public bool IsFavorited
        {
            get { return _isFavorited; }
            set { SetProperty(ref _isFavorited, value); }
        }

        #endregion

        #region IsShared

        private bool _isShared;

        public bool IsShared
        {
            get { return _isShared; }
            set { SetProperty(ref _isShared, value); }
        }

        #endregion

        #region IsCommented

        private bool _isCommented;

        public bool IsCommented
        {
            get { return _isCommented; }
            set { SetProperty(ref _isCommented, value); }
        }

        #endregion

        #region IsFlyoutOpened

        private bool _isFlyoutOpened;

        public bool IsFlyoutOpened
        {
            get { return _isFlyoutOpened; }
            set { SetProperty(ref _isFlyoutOpened, value); }
        }

        #endregion
    }
}