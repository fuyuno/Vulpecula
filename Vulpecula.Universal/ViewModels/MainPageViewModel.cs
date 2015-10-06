using Prism.Commands;
using Prism.Mvvm;

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
            this.Text = "Hello MVVM on UWP!";
        }

        #region Text変更通知プロパティ

        private string _Text;

        public string Text
        {
            get { return _Text; }
            set { this.SetProperty(ref this._Text, value); }
        }

        #endregion

        private DelegateCommand _authCommand;

        public DelegateCommand AuthCommand => this._authCommand ?? (this._authCommand = new DelegateCommand(Authorization));

        private async void Authorization()
        {
            var user = await this._croudiaProvider.Authorization();
            this.Text = $"Hello, @{user.ScreenName}!";
        }
    }
}