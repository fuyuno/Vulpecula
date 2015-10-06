using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using Vulpecula.Models;
using Vulpecula.Universal.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Vulpecula.Universal.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly CroudiaProvider _croudiaProvider;

        public MainPageViewModel()
        {
            this._croudiaProvider = new CroudiaProvider();
            // Initialize
            this.Initialize();
        }

        private void Initialize()
        {
            this.UserAccounts = new ObservableCollection<User>();
            this.IsHamburgerChecked = false;
            this.Text = "Hello MVVM on UWP!";
        }

        #region Properties

        public ObservableCollection<User> UserAccounts { get; set; }

        #region Text

        private string _text;

        public string Text
        {
            get { return this._text; }
            set { this.SetProperty(ref this._text, value); }
        }

        #endregion

        #region IsHamburgerChecked

        private bool _isHamburgerChecked;

        public bool IsHamburgerChecked
        {
            get { return this._isHamburgerChecked; }
            set { this.SetProperty(ref this._isHamburgerChecked, value); }
        }

        #endregion

        #endregion

        #region Commands

        #region AuthCommand

        private DelegateCommand _authCommand;

        public DelegateCommand AuthCommand
            => this._authCommand ?? (this._authCommand = new DelegateCommand(Authorization));

        private async void Authorization()
        {
            var user = await this._croudiaProvider.Authorization();
            this.UserAccounts.Add(user);
            this.Text = $"Hello, @{user.ScreenName}!";
        }

        #endregion

        #endregion

        #region Events

        public void OnChecked(object sender, RoutedEventArgs e) => this.IsHamburgerChecked = true;

        public void OnUnchecked(object sender, RoutedEventArgs e) => this.IsHamburgerChecked = false;

        public void PaneClosing(object sender, SplitViewPaneClosingEventArgs e) => this.IsHamburgerChecked = false;

        #endregion
    }
}