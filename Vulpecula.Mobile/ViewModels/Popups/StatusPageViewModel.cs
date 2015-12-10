using System.Linq;
using System.Windows.Input;

using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;

using Xamarin.Forms;
using Vulpecula.Models;

namespace Vulpecula.Mobile.ViewModels.Popups
{
    public class StatusPageViewModel : NavigationalViewModel
    {
        private readonly AccountManager _accountManager;

        public StatusPageViewModel(ILocalization localization, INavigationService navigationService, AccountManager accountManager)
            : base(localization, navigationService)
        {
            this._accountManager = accountManager;
            NavigationTitle = "StatusPage";
            this.MainText = "Hello!";
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                parameters["draft"] = this.Text;
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters == null)
            {
                return;
            }
            if (parameters.ContainsKey("draft"))
            {
                this.Text = parameters["draft"].ToString();
                parameters.Remove("draft");
            }
        }

        #region Properties

        #region MainText

        private string _mainText;

        public string MainText
        {
            get { return _mainText; }
            set { this.SetProperty(ref _mainText, value); }
        }

        #endregion

        #region Text

        private string _text;

        public string Text
        {
            get { return this._text; }
            set
            {
                if (this.SetProperty(ref this._text, value))
                {
                    this.SendCommand.ChangeCanExecute(); 
                }
            }
        }

        #endregion

        #endregion

        #region Commands


        #region CloseCommand

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                return this._closeCommand ?? (this._closeCommand = new Command(Close));
            }
        }

        private void Close()
        {
            this.NavigationService.GoBack();
        }

        #endregion

        #region SendCommand

        private Command _sendCommand;

        public Command SendCommand
        {
            get
            {
                return this._sendCommand ?? (this._sendCommand = new Command(Send, CanSend));
            }
        }

        private async void Send()
        {
            await this._accountManager.Providers.First().Croudia.Statuses.UpdateAsync(text => this.Text);
            this.Text = string.Empty;
            this.NavigationService.GoBack();
        }

        private bool CanSend()
        {
            if (string.IsNullOrWhiteSpace(this.Text) || this.Text.Length > 372)
            {
                return false;
            }
            return true;
        }

        #endregion

        #endregion
    }
}