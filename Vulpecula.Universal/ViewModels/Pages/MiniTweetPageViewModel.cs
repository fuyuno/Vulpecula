using System.Windows.Input;

using Prism.Commands;
using Prism.Windows.Navigation;

using Vulpecula.Universal.Models;

namespace Vulpecula.Universal.ViewModels.Pages
{
    public class MiniTweetPageViewModel : TweetPageViewModel
    {
        public MiniTweetPageViewModel(INavigationService navigationService, AccountManager accountManager) : base(navigationService, accountManager)
        {
            IsPublishedCloseRequest = false;
        }

        #region Properties

        private bool _isPublishedCloseRequest;

        public bool IsPublishedCloseRequest
        {
            get { return _isPublishedCloseRequest; }
            set { SetProperty(ref _isPublishedCloseRequest, value); }
        }

        #endregion Properties

        #region Commands

        #region CloseCommand

        private ICommand _closeCommand;
        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(Close));

        private void Close()
        {
            IsPublishedCloseRequest = true;
        }

        #endregion CloseCommand

        #endregion Commands
    }
}