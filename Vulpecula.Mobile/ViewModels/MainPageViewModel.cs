using System.Diagnostics;
using System.Threading.Tasks;

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
        private bool _isFirst;
        public StatusTimelineViewModel PublicTimelineViewModel { get; }
        public StatusTimelineViewModel HomeTimelineViewModel { get; }
        public StatusTimelineViewModel MentionsTimelineViewModel { get; }

        public MainPageViewModel(ILocalization localization, INavigationService navigationService, AccountManager accountManager) : base(localization, navigationService)
        {
            this._accountManager = accountManager;
            PublicTimelineViewModel = new StatusTimelineViewModel(localization, navigationService, "Public", "public", "PublicTimeline");
            HomeTimelineViewModel = new StatusTimelineViewModel(localization, navigationService, "Home", "home", "HomeTimeline");
            MentionsTimelineViewModel = new StatusTimelineViewModel(localization, navigationService, "Mentions", "mention", "MentionsTimeline");

            if (false)
            {
                // リセット用
                this._accountManager.ResetAccounts();

                var vault = App.ModelLocator.GetModel<IPasswordVault>();
                var credential = App.ModelLocator.GetModel<IPasswordCredentials>();
                credential.UserName = "MikazukiFuyuno";
                vault.Remove(credential);
            }

            var task = Task.Factory.StartNew(
                async () =>
                {
                    await this._accountManager.InitializeAccounts();
                    if (this._accountManager.Providers.Count == 0)
                    {
                        // なんか良い方法ないかね
                        this._isFirst = true;
                    }
                    else
                    {
                        foreach (var user in this._accountManager.Users)
                        {
                            Debug.WriteLine(user.ScreenName);
                        }
                    }
                });
            task.Wait();
        }

        public void Initialize()
        {
            if (this._isFirst)
            {
                this.NavigationService.Navigate<AuthorizationPage>();
                this._isFirst = false;
            }
        }
    }
}