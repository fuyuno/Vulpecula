using JetBrains.Annotations;

using Prism.Windows.Navigation;

namespace Vulpecula.Universal.ViewModels
{
    [UsedImplicitly]
    public class CommandMenuPageViewModel : MenuViewModel
    {
        public CommandMenuPageViewModel(INavigationService navigationService) : base(navigationService) {}
    }
}