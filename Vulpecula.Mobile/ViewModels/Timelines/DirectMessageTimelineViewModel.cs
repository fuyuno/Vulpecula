using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Prism.Commands;
using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.ViewModels.Timelines.Primitives;
using Vulpecula.Mobile.Views.Popups;
using Vulpecula.Streaming.Reactive;

// ReSharper disable InconsistentNaming

namespace Vulpecula.Mobile.ViewModels.Timelines
{
    public class DirectMessageTimelineViewModel : TabbedViewModel
    {
        private readonly CroudiaProvider _provider;
        private IDisposable _disposable;
        private long _receivedLastId;
        private long _sentLastId;
        public ObservableCollection<DirectMailViewModel> Statuses { get; }

        public DirectMessageTimelineViewModel(ILocalization localization, INavigationService navigationService, CroudiaProvider provider)
        : base(localization, navigationService)
        {
            Title = GetLocalizedString("Messages");
            Icon = "message";
            NavigationTitle = GetLocalizedString("MessagesTimeline");
            Statuses = new ObservableCollection<DirectMailViewModel>();
            _provider = provider;
        }

        public override void OnTabNavigatedFrom()
        {
            _disposable?.Dispose();
        }

        public override void OnTabNavigatedTo()
        {
            var obs = _provider.Croudia.SecretMails.ReceivedAsObservable(since_id => _receivedLastId);
            obs.Merge(_provider.Croudia.SecretMails.SentAsObservable(since_id => _sentLastId));
            _disposable = obs.Subscribe(w =>
            {
                Statuses.Insert(0, new DirectMailViewModel(Localization, NavigationService, w));
                if (w.SenderId == _provider.User.Id)
                    _sentLastId = w.Id;
                else
                    _receivedLastId = w.Id;
            });
        }

        #region NavigateCommand

        private DelegateCommand _navigateCommand;

        public DelegateCommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand(Navigate));

        private void Navigate()
        {
            NavigationService.Navigate<StatusPage>();
        }

        #endregion
    }
}