using Vulpecula.Mobile.ViewModels.Timelines.Primitives;

namespace Vulpecula.Mobile.ViewModels.Timelines
{
    public class DirectMessagePageViewModel : TabbedViewModel
    {
        public DirectMessagePageViewModel()
        {
            Title = "Messages";
            Icon = "message";
            NavigationTitle = "Messages";
        }
    }
}