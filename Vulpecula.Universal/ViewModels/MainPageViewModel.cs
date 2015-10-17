using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Prism.Windows.Mvvm;

using Vulpecula.Universal.Models;
using Vulpecula.Universal.ViewModels.Timelines;

namespace Vulpecula.Universal.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public AccountManager AccountManager { get; }

        public ColumnManager ColumnManager { get; }

        public MainPageViewModel()
        {
            this.Timelines = new ObservableCollection<TimelineViewModelBase>();
            this.IsHamburgerChecked = false;
            this.AccountManager = new AccountManager();
            this.ColumnManager = new ColumnManager();
        }

        private async Task Initialize()
        {
            // this.AccountManager.ResetAccounts();
            // this.ColumnManager.ClearColumns();

            await this.AccountManager.InitializeAccounts();
            this.ColumnManager.InitializeColumns();

            if (this.AccountManager.Users.Count == 0)
            {
                await this.Authorization();
                if (this.AccountManager.Users.Count > 0)
                    this.ColumnManager.SetupInitialColumns(this.AccountManager.Users[0].Id);
            }
        }

        private async Task Authorization()
        {
            await this.AccountManager.AuthorizationAccount();
        }

        #region Properties

        public ObservableCollection<TimelineViewModelBase> Timelines { get; }

        #region IsHamburgerChecked

        private bool _isHamburgerChecked;

        public bool IsHamburgerChecked
        {
            get { return this._isHamburgerChecked; }
            set { this.SetProperty(ref this._isHamburgerChecked, value); }
        }

        #endregion

        #endregion

        #region Events

        public async void OnLoaded() => await this.Initialize();

        public void OnChecked() => this.IsHamburgerChecked = true;

        public void OnUnchecked() => this.IsHamburgerChecked = false;

        public void PaneClosing() => this.IsHamburgerChecked = false;

        public async void OnTapped(object sender, RoutedEventArgs e) => await this.Authorization();

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
            => ((ListBox) sender).SelectedIndex = -1;

        #endregion
    }
}