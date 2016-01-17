using System;
using System.Diagnostics;
using System.Linq;

using JetBrains.Annotations;

using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.ViewModels.Timelines;
using Vulpecula.Mobile.Views;

// ReSharper disable HeuristicUnreachableCode

#pragma warning disable 162

namespace Vulpecula.Mobile.ViewModels
{
    [UsedImplicitly]
    public class MainPageViewModel : NavigationalViewModel
    {
        private readonly AccountManager _accountManager;
        private bool _isFirst;

        public MainPageViewModel(ILocalization localization, INavigationService navigationService, AccountManager accountManager)
        : base(localization, navigationService)
        {
            _accountManager = accountManager;

            if (false)
                ResetAllConfigurations();
        }

        private void ResetAllConfigurations()
        {
            _accountManager.ResetAccounts();

            var vault = App.ModelLocator.GetModel<IPasswordVault>();
            var credential = App.ModelLocator.GetModel<IPasswordCredentials>();
            credential.UserName = "MikazukiFuyuno";
            vault.Remove(credential);
        }

        public void Initialize()
        {
            // 同期的に処理しても問題なさ気
            try
            {
                _accountManager.InitializeAccounts();
                if (_accountManager.Providers.Count == 0)
                {
                    // なんか良い方法ないかね
                    _isFirst = true;
                    return;
                }
                PublicTimelineViewModel = new StatusTimelineViewModel(Localization, NavigationService, _accountManager.Providers.First(), TimelineTypes.Public);
                MentionsTimelineViewModel = new StatusTimelineViewModel(Localization, NavigationService, _accountManager.Providers.First(), TimelineTypes.Mentions);
                MessagesTimelineViewModel = new DirectMessageTimelineViewModel(Localization, NavigationService, _accountManager.Providers.First());
            }
            catch (Exception)
            {
                Debug.WriteLine("throw Exception;");
                ResetAllConfigurations();
                _isFirst = true;
            }
        }

        public void OnAppearing()
        {
            if (_isFirst)
            {
                NavigationService.Navigate<AuthorizationPage>();
                _isFirst = false;
            }
            if (PublicTimelineViewModel == null && _accountManager.Users.Count != 0)
            {
                PublicTimelineViewModel = new StatusTimelineViewModel(Localization, NavigationService, _accountManager.Providers.First(), TimelineTypes.Public);
                MentionsTimelineViewModel = new StatusTimelineViewModel(Localization, NavigationService, _accountManager.Providers.First(), TimelineTypes.Mentions);
                MessagesTimelineViewModel = new DirectMessageTimelineViewModel(Localization, NavigationService, _accountManager.Providers.First());
            }
        }

        #region Properties

        #region PublicTimelineViewModel

        private StatusTimelineViewModel _publicTimelineViewModel;

        public StatusTimelineViewModel PublicTimelineViewModel
        {
            get { return _publicTimelineViewModel; }
            set { SetProperty(ref _publicTimelineViewModel, value); }
        }

        #endregion

        #region MentionsTimelineViewModel

        private StatusTimelineViewModel _mentionsTimelineViewModel;

        public StatusTimelineViewModel MentionsTimelineViewModel
        {
            get { return _mentionsTimelineViewModel; }
            set { SetProperty(ref _mentionsTimelineViewModel, value); }
        }

        #endregion

        #region MessagesTimelineViewModel

        private DirectMessageTimelineViewModel _messagesTimelineViewModel;

        public DirectMessageTimelineViewModel MessagesTimelineViewModel
        {
            get { return _messagesTimelineViewModel; }
            set { SetProperty(ref _messagesTimelineViewModel, value); }
        }

        #endregion

        #endregion
    }
}