using System.Diagnostics;

using JetBrains.Annotations;

using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.ViewModels.Timelines;
using Vulpecula.Mobile.Views;

namespace Vulpecula.Mobile.ViewModels
{
    [UsedImplicitly]
    public class MainPageViewModel : NavigationalViewModel
    {
        private readonly AccountManager _accountManager;
        public StatusTimelineViewModel PublicTimelineViewModel { get; }
        public StatusTimelineViewModel HomeTimelineViewModel { get; }
        public StatusTimelineViewModel MentionsTimelineViewModel { get; }

        public MainPageViewModel(ILocalization localization, INavigationService navigationService, AccountManager accountManager) : base(localization, navigationService)
        {
            this._accountManager = accountManager;
            PublicTimelineViewModel = new StatusTimelineViewModel(localization, navigationService, "Public", "public", "PublicTimeline");
            HomeTimelineViewModel = new StatusTimelineViewModel(localization, navigationService, "Home", "home", "HomeTimeline");
            MentionsTimelineViewModel = new StatusTimelineViewModel(localization, navigationService, "Mentions", "mention", "MentionsTimeline");
        }

        public async void Initialize()
        {
            await this._accountManager.InitializeAccounts();

            if (this._accountManager.Providers.Count == 0)
            {
                this.NavigationService.Navigate<AuthorizationPage>();
            }
            else
            {
                foreach (var user in this._accountManager.Users)
                {
                    Debug.WriteLine(user.ScreenName);
                }
            }
        }
    }
}