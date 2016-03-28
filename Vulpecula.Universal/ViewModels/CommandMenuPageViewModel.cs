using JetBrains.Annotations;

using Prism.Windows.Navigation;

namespace Vulpecula.Universal.ViewModels
{
    [UsedImplicitly]
    public class CommandMenuPageViewModel : MenuViewModel
    {
        public CommandMenuPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        #region Overrides of MenuViewModel

        protected override void NavigateToWhisperPage()
        {
            NavigationService.Navigate("Pages.Tweet", null);
            EventFired = true;
        }

        #endregion Overrides of MenuViewModel
    }
}