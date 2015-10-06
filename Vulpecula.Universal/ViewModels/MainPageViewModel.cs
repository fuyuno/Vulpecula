using System.Collections.ObjectModel;

using Prism.Commands;
using Prism.Mvvm;

using Vulpecula.Models;
using Vulpecula.Universal.Models;

namespace Vulpecula.Universal.ViewModels
{
    internal class MainPageViewModel : BindableBase
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
            this.Text = "Hello MVVM on UWP!";
        }

        #region Properties

        public ObservableCollection<User> UserAccounts { get; set; }

        #region Text

        private string _Text;

        public string Text
        {
            get { return _Text; }
            set { this.SetProperty(ref this._Text, value); }
        }

        #endregion

        #endregion

        #region Commands
        #region AuthCommand
        private DelegateCommand _authCommand;

        public DelegateCommand AuthCommand => this._authCommand ?? (this._authCommand = new DelegateCommand(Authorization));

        private async void Authorization()
        {
            var user = await this._croudiaProvider.Authorization();
            this.UserAccounts.Add(user);
            this.Text = $"Hello, @{user.ScreenName}!";
        }

        #endregion
        #endregion
    }
}