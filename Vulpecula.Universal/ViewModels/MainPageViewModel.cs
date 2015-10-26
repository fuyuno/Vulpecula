using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Vulpecula.Models;
using Vulpecula.Universal.Extensions;
using Vulpecula.Universal.Helpers;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Services;
using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.ViewModels.Primitives;
using Vulpecula.Universal.ViewModels.Timelines;

namespace Vulpecula.Universal.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private readonly AccountManager _accountManager;

        private readonly ColumnManager _columnManager;

        public MainPageViewModel()
        {
            this.IsHamburgerChecked = false;
            this.IsWhisperZoneOpened = false;
            this.Users = new ObservableCollection<UserAccountViewModel>();
            this.Columns = new ObservableCollection<ColumnViewModel>();
            this._accountManager = new AccountManager();
            this._columnManager = new ColumnManager();

            ViewModelHelper.SubscribeNotifyCollectionChanged(this._accountManager.Users, this.Users, (User w) =>
                UserAccountViewModel.Create(this._accountManager.Providers, w)).AddTo(this);
            ViewModelHelper.SubscribeNotifyCollectionChanged(this._columnManager.Columns, this.Columns, (Column w) =>
                ColumnViewModel.Create(this._accountManager.Providers, w)).AddTo(this);
        }

        private async Task Initialize()
        {
            // this._accountManager.ResetAccounts();
            // this._columnManager.ClearColumns();

            await this._accountManager.InitializeAccounts();
            this._columnManager.InitializeColumns();

            if (this._accountManager.Users.Count == 0)
            {
                await this.Authorization();
                if (this._accountManager.Users.Count > 0)
                    this._columnManager.SetupInitialColumns(this._accountManager.Users[0].Id);
            }
        }

        private async Task Authorization()
        {
            await this._accountManager.AuthorizationAccount();
        }

        #region Properties

        public ObservableCollection<ColumnViewModel> Columns { get; }

        public ObservableCollection<UserAccountViewModel> Users { get; }

        #region IsHamburgerChecked

        private bool _isHamburgerChecked;

        public bool IsHamburgerChecked
        {
            get { return this._isHamburgerChecked; }
            set { this.SetProperty(ref this._isHamburgerChecked, value); }
        }

        #endregion

        #region IsWhisperZoneOpened

        private bool _isWhisperZoneOpended;

        public bool IsWhisperZoneOpened
        {
            get { return this._isWhisperZoneOpended; }
            set { this.SetProperty(ref this._isWhisperZoneOpended, value); }
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

        #endregion

        #region Events

        public async void OnLoaded() => await this.Initialize();

        public void OnUnloaded()
        {
            ServiceProvider.SuspendService();
            this.Dispose();
        }

        public void OnChecked()
        {
            if (this.IsHamburgerChecked)
                return;
            this.IsHamburgerChecked = true;
            this.IsWhisperZoneOpened = false;
        }

        public void OnUnchecked()
        {
            this.IsHamburgerChecked = false;
            this.IsWhisperZoneOpened = false;
        }

        public void PaneClosing()
        {
            this.IsHamburgerChecked = false;
            this.IsWhisperZoneOpened = false;
        }

        public async void OnTappedAuthorization(object sender, RoutedEventArgs e) => await this.Authorization();

        public void OnTappedToggleWhisperZone(object sender, RoutedEventArgs e)
        {
            this.IsWhisperZoneOpened = !this.IsWhisperZoneOpened;
            if (this.IsWhisperZoneOpened && !this.IsHamburgerChecked)
                this.IsHamburgerChecked = true;
        }

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
            => ((ListBox)sender).SelectedIndex = -1;

        public void OnClickedSendWhisper()
        {
            foreach (var user in this.Users)
            {
                if (user.IsWhisperEnabled)
                    user.SendWhisper(this.Text);
            }
            this.Text = string.Empty;
        }

        #endregion
    }
}