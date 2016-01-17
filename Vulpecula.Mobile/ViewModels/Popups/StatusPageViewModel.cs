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
        private long _commentStatusId;
        private long _inReplyToStatusId;

        public StatusPageViewModel(ILocalization localization, INavigationService navigationService, AccountManager accountManager)
        : base(localization, navigationService)
        {
            _accountManager = accountManager;
            NavigationTitle = GetLocalizedString("Status");
            Counter = 372;
            _inReplyToStatusId = -1;
            _commentStatusId = -1;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters == null)
                return;
            if (parameters.ContainsKey("status"))
                Text = parameters["status"].ToString();
            if (parameters.ContainsKey("in_reply_to_status_id"))
                _inReplyToStatusId = (long) parameters["in_reply_to_status_id"];
            if (parameters.ContainsKey("comment"))
                _commentStatusId = (long) parameters["comment"];
        }

        public void TextChanged()
        {
            Counter = 372 - Text.Length;
        }

        #region Properties

        #region Text

        private string _text;

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        #endregion

        #region Counter

        private int _counter;

        public int Counter
        {
            get { return _counter; }
            set
            {
                if (SetProperty(ref _counter, value))
                    SendCommand.ChangeCanExecute();
            }
        }

        #endregion

        #endregion

        #region Commands

        #region CloseCommand

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new Command(Close)); }
        }

        private void Close()
        {
            NavigationService.GoBack();
        }

        #endregion

        #region SendCommand

        private Command _sendCommand;

        public Command SendCommand
        {
            get { return _sendCommand ?? (_sendCommand = new Command(Send, CanSend)); }
        }

        private async void Send()
        {
            if (_inReplyToStatusId > -1)
                await _accountManager.Providers.First().Croudia.Statuses.UpdateAsync(status => Text, in_reply_to_status_id => _inReplyToStatusId);
            if (_commentStatusId > -1)
                await _accountManager.Providers.First().Croudia.Statuses.CommentAsync(status => Text, id => _commentStatusId);
            if (_inReplyToStatusId == -1 && _commentStatusId == -1)
                await _accountManager.Providers.First().Croudia.Statuses.UpdateAsync(status => Text);
            Text = string.Empty;
            NavigationService.GoBack();
        }

        private bool CanSend()
        {
            if (string.IsNullOrWhiteSpace(Text) || Counter > 372 || Counter < 0)
                return false;
            return true;
        }

        #endregion

        #endregion
    }
}