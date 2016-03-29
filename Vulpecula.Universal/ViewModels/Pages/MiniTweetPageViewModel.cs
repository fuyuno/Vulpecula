using Prism.Windows.Navigation;

using Vulpecula.Universal.Models;

namespace Vulpecula.Universal.ViewModels.Pages
{
    public class MiniTweetPageViewModel : TweetPageViewModel
    {
        public MiniTweetPageViewModel(INavigationService navigationService, AccountManager accountManager) : base(navigationService, accountManager)
        {
        }
    }
}