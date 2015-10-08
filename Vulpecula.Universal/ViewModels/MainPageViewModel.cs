using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Windows.Security.Credentials;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Prism.Mvvm;

using Vulpecula.Universal.Models;

namespace Vulpecula.Universal.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private int _accountCount;

        public MainPageViewModel()
        {
            this.UserAccounts = new ObservableCollection<CroudiaProvider>();
            this.Text = "Hello MVVM on UWP!";
            this.IsHamburgerChecked = false;
        }

        private async Task Initialize()
        {
            try
            {
                var vault = new PasswordVault();
                vault.RetrieveAll();
                var accounts = vault.FindAllByResource(AppDefintions.VulpeculaAppKey);
                foreach (var credential in accounts)
                {
                    var provider = new CroudiaProvider();
                    if (!await provider.Authorization(vault, credential))
                    {
                        vault.Remove(credential);
                        continue;
                    }
                    this.UserAccounts.Add(provider);
                    this._accountCount++;
                }
            }
            catch (COMException)
            {
                // ignored
            }
        }

        private async Task Authorization()
        {
            // TODO: Wrong ViewModel
            if (this._accountCount >= 10)
            {
                var dialog = new MessageDialog("これ以上アカウントを追加することはできません。", "内部エラー");
                await dialog.ShowAsync();
                return;
            }
            var provider = new CroudiaProvider();
            if (!await provider.Authorization(new PasswordVault(), null))
                return;
            this.UserAccounts.Add(provider);
        }

        #region Properties

        public ObservableCollection<CroudiaProvider> UserAccounts { get; }

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

        #region Events

        // TODO: Remove Code-behind
        public async void OnLoaded(object sender, RoutedEventArgs e) => await this.Initialize();

        public void OnChecked(object sender, RoutedEventArgs e) => this.IsHamburgerChecked = true;

        public void OnUnchecked(object sender, RoutedEventArgs e) => this.IsHamburgerChecked = false;

        public void PaneClosing(object sender, SplitViewPaneClosingEventArgs e) => this.IsHamburgerChecked = false;

        public async void OnTapped(object sender, RoutedEventArgs e) => await this.Authorization();

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
            => ((ListBox) sender).SelectedIndex = -1;

        #endregion
    }
}