using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;

namespace Vulpecula.Mobile.ViewModels.Timelines
{
    public class DirectMessageTimelineViewModel : TabbedViewModel
    {
        private readonly CroudiaProvider _provider;

        public DirectMessageTimelineViewModel(ILocalization localization, INavigationService navigationService, CroudiaProvider provider)
            : base(localization, navigationService)
        {
            Title = this.GetLocalizedString("Messages");
            Icon = "message";
            NavigationTitle = this.GetLocalizedString("MessagesTimeline");
            this._provider = provider;
        }
    }
}