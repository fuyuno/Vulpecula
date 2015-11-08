using Prism.Navigation;

using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;

namespace Vulpecula.Mobile.ViewModels.Timelines
{
    public class DirectMessageTimelineViewModel : TabbedViewModel
    {
        public DirectMessageTimelineViewModel(ILocalization localization, INavigationService navigationService)
            : base(localization, navigationService)
        {
            Title = this.GetLocalizedString("Messages");
            Icon = "message";
            NavigationTitle = this.GetLocalizedString("MessagesTimeline");
        }
    }
}