using Prism.Windows.Navigation;

namespace Vulpecula.Universal.ViewModels.Pages
{
    public class MiniTweetPageViewModel : TweetPageViewModel
    {
        public MiniTweetPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}