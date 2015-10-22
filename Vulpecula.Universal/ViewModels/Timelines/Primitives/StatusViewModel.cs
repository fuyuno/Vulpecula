using Vulpecula.Models;
using Vulpecula.Universal.Models.Timelines.Primitive;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Timelines.Primitives
{
    public class StatusViewModel : ViewModel
    {
        public StatusModel Model { get; }

        public bool IsShare { get; }
        public bool IsComment { get; }
        public bool IsDirectMessage { get; }

        public StatusViewModel(StatusModel statusModel)
        {
            this.Model = statusModel;
            if (this.Model.IsDirectMessage)
            {
                this.IsDirectMessage = this.Model.IsDirectMessage;
                return;
            }

            this.IsShare = this.Model.IsSpread;
            this.IsComment = this.Model.IsSpread && !string.IsNullOrWhiteSpace(this.Model.Text);
        }

        private UserViewModel CreateUserViewModel(User user)
        {
            var uvm = new UserViewModel(user);
            this.CompositeDisposable.Add(uvm);
            return uvm;
        }

        #region Text

        private string _text;

        public string Text => this._text ?? (this._text = this.IsShare ? this.Model.SpreadStatus.Text : this.Model.Text);

        #endregion

        #region User

        private UserViewModel _user;

        public UserViewModel User => this._user ?? (this._user = CreateUserViewModel(this.Model.User));

        #endregion

        #region Recipient

        private UserViewModel _recipient;
        public UserViewModel Recipient => this._recipient ?? (this._recipient = CreateUserViewModel(this.Model.Recipient));

        #endregion

        #region IsFavorited

        private bool _isFavorited;

        public bool IsFavorited
        {
            get { return this._isFavorited; }
            set { this.SetProperty(ref this._isFavorited, value); }
        }

        #endregion

        #region IsShared

        private bool _isShared;

        public bool IsShared
        {
            get { return this._isShared; }
            set { this.SetProperty(ref this._isShared, value); }
        }

        #endregion

        #region IsCommented

        private bool _isCommented;

        public bool IsCommented
        {
            get { return this._isCommented; }
            set { this.SetProperty(ref this._isCommented, value); }
        }

        #endregion
    }
}