using JetBrains.Annotations;

using Prism.Windows.Navigation;

using Vulpecula.Universal.Models;

namespace Vulpecula.Universal.ViewModels
{
    [UsedImplicitly]
    public class CommandMenuPageViewModel : MenuViewModel
    {
        public CommandMenuPageViewModel(INavigationService navigationService, AccountManager accountManager) : base(navigationService, accountManager)
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