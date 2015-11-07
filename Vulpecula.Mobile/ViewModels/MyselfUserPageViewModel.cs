using Vulpecula.Mobile.ViewModels.Timelines.Primitives;

namespace Vulpecula.Mobile.ViewModels
{
    public class MyselfUserPageViewModel : TabbedViewModel
    {
        public MyselfUserPageViewModel()
        {
            Title = "Me";
            Icon = "user";
            NavigationTitle = "Me";
        }
    }
}