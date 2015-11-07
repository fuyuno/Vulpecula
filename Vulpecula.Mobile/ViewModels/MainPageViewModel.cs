using JetBrains.Annotations;

using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.ViewModels.Timelines;

namespace Vulpecula.Mobile.ViewModels
{
    [UsedImplicitly]
    public class MainPageViewModel : ViewModel
    {
        public StatusTimelineViewModel PublicTimelineViewModel { get; }
        public StatusTimelineViewModel HomeTimelineViewModel { get; }
        public StatusTimelineViewModel MentionsTimelineViewModel { get; }
        public DirectMessagePageViewModel MessageTimelineViewModel { get; }
        public MyselfUserPageViewModel MyselfUserPageViewModel { get; }

        public MainPageViewModel()
        {
            PublicTimelineViewModel = new StatusTimelineViewModel("Public", "public", "Public Timeline");
            HomeTimelineViewModel = new StatusTimelineViewModel("Home", "home", "Home Timeline");
            MentionsTimelineViewModel = new StatusTimelineViewModel("Mentions", "mention", "Mentions");
            MessageTimelineViewModel = new DirectMessagePageViewModel();
            MyselfUserPageViewModel = new MyselfUserPageViewModel();
        }
    }
}