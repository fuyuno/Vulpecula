using System.Collections.ObjectModel;

using JetBrains.Annotations;

using Vulpecula.Models;
using Vulpecula.Universal.Helpers;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels
{
    [UsedImplicitly]
    public class MenuViewModel : ViewModel
    {
        public MenuViewModel()
        {
            this.Accounts = new ObservableCollection<UserAccountViewModel>();
            this.Text = string.Empty;
            this.IsWhisperZoneOpened = false;
            ViewModelHelper.SubscribeNotifyCollectionChanged(AccountManager.Instance.Users, this.Accounts, (User w) => UserAccountViewModel.Create(w));
        }

        #region Properties

        public ObservableCollection<UserAccountViewModel> Accounts { get; }

        #region Text Property

        private string _text;

        public string Text
        {
            get { return this._text; }
            set { this.SetProperty(ref this._text, value); }
        }

        #endregion

        #region IsWhisperZoneOpened

        private bool _isWhisperZoneOpened;

        public bool IsWhisperZoneOpened
        {
            get { return this._isWhisperZoneOpened; }
            set { this.SetProperty(ref this._isWhisperZoneOpened, value); }
        }

        #endregion

        #endregion
    }
}