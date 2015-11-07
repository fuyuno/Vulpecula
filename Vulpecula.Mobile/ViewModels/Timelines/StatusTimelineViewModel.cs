using Vulpecula.Mobile.ViewModels.Timelines.Primitives;

namespace Vulpecula.Mobile.ViewModels.Timelines
{
    public class StatusTimelineViewModel : TabbedViewModel
    {
        public StatusTimelineViewModel(string title, string icon, string navigationTitle = "")
        {
            Title = title;
            Icon = icon;
            NavigationTitle = string.IsNullOrWhiteSpace(navigationTitle) ? title : navigationTitle;
        }
    }
}