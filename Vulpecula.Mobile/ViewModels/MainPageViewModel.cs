using JetBrains.Annotations;

using Prism.Navigation;

using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.ViewModels.Timelines;

namespace Vulpecula.Mobile.ViewModels
{
    [UsedImplicitly]
    public class MainPageViewModel : NavigationalViewModel
    {
        public StatusTimelineViewModel PublicTimelineViewModel { get; }
        public StatusTimelineViewModel HomeTimelineViewModel { get; }
        public StatusTimelineViewModel MentionsTimelineViewModel { get; }

        public MainPageViewModel(ILocalization localization, INavigationService navigationService) : base(localization, navigationService)
        {
            PublicTimelineViewModel = new StatusTimelineViewModel(localization, navigationService, "Public", "public", "PublicTimeline");
            HomeTimelineViewModel = new StatusTimelineViewModel(localization, navigationService, "Home", "home", "HomeTimeline");
            MentionsTimelineViewModel = new StatusTimelineViewModel(localization, navigationService, "Mentions", "mention", "MentionsTimeline");
        }
    }
}