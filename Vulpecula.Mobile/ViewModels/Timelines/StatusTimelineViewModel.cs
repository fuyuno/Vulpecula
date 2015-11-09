using Prism.Navigation;

using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;

namespace Vulpecula.Mobile.ViewModels.Timelines
{
    public class StatusTimelineViewModel : TabbedViewModel
    {
        public StatusTimelineViewModel(ILocalization localization, INavigationService navigationService, string title, string icon, string navigationTitle = "")
            : base(localization, navigationService)
        {
            Title = this.GetLocalizedString(title);
            Icon = GetLocalizedString(icon);
            NavigationTitle = GetLocalizedString(string.IsNullOrWhiteSpace(navigationTitle) ? title : navigationTitle);
        }
    }
}