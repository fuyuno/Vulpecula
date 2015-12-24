using System.Linq;
using System.Windows.Input;

using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;

using Xamarin.Forms;

namespace Vulpecula.Mobile.ViewModels.Popups
{
    // TODO: Draft
    public class StatusPageViewModel : NavigationalViewModel
    {
        private readonly AccountManager _accountManager;
        private long _inReplyToStatusId;

        public StatusPageViewModel(ILocalization localization, INavigationService navigationService, AccountManager accountManager)
            : base(localization, navigationService)
        {
            this._accountManager = accountManager;
            NavigationTitle = this.GetLocalizedString("Status");
            this.Counter = 372;
            this._inReplyToStatusId = -1;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters == null)
            {
                return;
            }
            if (parameters.ContainsKey("status"))
            {
                this.Text = parameters["status"].ToString();
            }
            if (parameters.ContainsKey("in_reply_to_status_id"))
            {
                this._inReplyToStatusId = (long)parameters["in_reply_to_status_id"];
            }
        }

        public void TextChanged()
        {
            this.Counter = 372 - this.Text.Length;
        }

        #region Properties

        #region Text

        private string _text;

        public string Text
        {
            get { return this._text; }
            set{ this.SetProperty(ref this._text, value); }
        }

        #endregion

        #region Counter

        private int _counter;

        public int Counter
        {
            get { return this._counter; }
            set
            {
                if (this.SetProperty(ref this._counter, value))
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
            get { return this._closeCommand ?? (this._closeCommand = new Command(Close)); }
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
            get { return this._sendCommand ?? (this._sendCommand = new Command(Send, CanSend)); }
        }

        private async void Send()
        {
            if (this._inReplyToStatusId == -1)
            {
                await this._accountManager.Providers.First().Croudia.Statuses.UpdateAsync(status => this.Text);
            }
            else
            {
                await this._accountManager.Providers.First().Croudia.Statuses.UpdateAsync(status => this.Text, in_reply_to_status_id => this._inReplyToStatusId);
            }
            this.Text = string.Empty;
            this.NavigationService.GoBack();
        }

        private bool CanSend()
        {
            if (string.IsNullOrWhiteSpace(this.Text) || this.Counter > 372 || this.Counter < 0)
            {
                return false;
            }
            return true;
        }

        #endregion

        #endregion
    }
}