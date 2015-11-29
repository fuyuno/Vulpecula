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
                this.PublicTimelineViewModel = new StatusTimelineViewModel(this.Localization, this.NavigationService, this._accountManager.Providers.First(), "Public", "public");
                this.HomeTimelineViewModel = new StatusTimelineViewModel(this.Localization, this.NavigationService, this._accountManager.Providers.First(), "Home", "home");
                this.MentionsTimelineViewModel = new StatusTimelineViewModel(this.Localization, this.NavigationService, this._accountManager.Providers.First(), "Mentions", "mention");
                this.MessagesTimelineViewModel = new DirectMessageTimelineViewModel(this.Localization, this.NavigationService, this._accountManager.Providers.First());
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
                this.PublicTimelineViewModel = new StatusTimelineViewModel(this.Localization, this.NavigationService, this._accountManager.Providers.First(), "Public", "public");
                this.HomeTimelineViewModel = new StatusTimelineViewModel(this.Localization, this.NavigationService, this._accountManager.Providers.First(), "Home", "home");
                this.MentionsTimelineViewModel = new StatusTimelineViewModel(this.Localization, this.NavigationService, this._accountManager.Providers.First(), "Mentions", "mention");
                this.MessagesTimelineViewModel = new DirectMessageTimelineViewModel(this.Localization, this.NavigationService, this._accountManager.Providers.First());
                this.UserDetailsPageViewModel = new UserDetailsPageViewModel(this.Localization, this.NavigationService, this._accountManager, this._accountManager.Users.First());
            }
        }

        #region Properties

        #region PublicTimelineViewModel

        private StatusTimelineViewModel _publicTimelineViewModel;

        public StatusTimelineViewModel PublicTimelineViewModel
        {
            get { return this._publicTimelineViewModel; }
            set { this.SetProperty(ref this._publicTimelineViewModel, value); }
        }

        #endregion

        #region HomeTimelineViewModel

        private StatusTimelineViewModel _homeTimelineViewModel;

        public StatusTimelineViewModel HomeTimelineViewModel
        {
            get { return this._homeTimelineViewModel; }
            set { this.SetProperty(ref this._homeTimelineViewModel, value); }
        }

        #endregion

        #region MentionsTimelineViewModel

        private StatusTimelineViewModel _mentionsTimelineViewModel;

        public StatusTimelineViewModel MentionsTimelineViewModel
        {
            get { return this._mentionsTimelineViewModel; }
            set { this.SetProperty(ref this._mentionsTimelineViewModel, value); }
        }

        #endregion

        #region MessagesTimelineViewModel

        private DirectMessageTimelineViewModel _messagesTimelineViewModel;

        public DirectMessageTimelineViewModel MessagesTimelineViewModel
        {
            get { return this._messagesTimelineViewModel; }
            set { this.SetProperty(ref this._messagesTimelineViewModel, value); }
        }

        #endregion

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