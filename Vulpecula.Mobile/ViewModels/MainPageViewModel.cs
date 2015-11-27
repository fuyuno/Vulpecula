using System;
using System.Diagnostics;
using System.Linq;

using JetBrains.Annotations;

using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Pages;
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
        }

        public void Initialize()
        {
            // 同期的に処理しても問題なさ気
            try
            {
                this._accountManager.InitializeAccounts();
                if (this._accountManager.Providers.Count == 0)
                {
                    // なんか良い方法ないかね
                    this._isFirst = true;
                    return;
                }
                this.UserDetailsPageViewModel = new UserDetailsPageViewModel(this.Localization, this.NavigationService, this._accountManager, this._accountManager.Users.First());
            }
            catch (Exception)
            {
                // ignored
                Debug.WriteLine("throw Exception;");
            }
        }

        public void OnAppearing()
        {
            if (this._isFirst)
            {
                this.NavigationService.Navigate<AuthorizationPage>();
                this._isFirst = false;
            }
            if (this.UserDetailsPageViewModel == null && this._accountManager.Users.Count != 0)
            {
                this.UserDetailsPageViewModel = new UserDetailsPageViewModel(this.Localization, this.NavigationService, this._accountManager, this._accountManager.Users.First());
            }
        }

        #region Properties

        public StatusTimelineViewModel PublicTimelineViewModel { get; }
        public StatusTimelineViewModel HomeTimelineViewModel { get; }
        public StatusTimelineViewModel MentionsTimelineViewModel { get; }

        #region UserDetailsPageViewModel

        private UserDetailsPageViewModel _userDetailsPageViewModel;

        public UserDetailsPageViewModel UserDetailsPageViewModel
        {
            get { return this._userDetailsPageViewModel; }
            private set { this.SetProperty(ref this._userDetailsPageViewModel, value); }
        }

        #endregion

        #endregion
    }
}