using System;
using System.Collections.ObjectModel;

using Prism.Commands;
using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.ViewModels.Timelines.Primitives;
using Vulpecula.Mobile.Views.Popups;
using Vulpecula.Models;
using Vulpecula.Streaming.Reactive;

// ReSharper disable InconsistentNaming

namespace Vulpecula.Mobile.ViewModels.Timelines
{
    public sealed class StatusTimelineViewModel : TabbedViewModel
    {
        private readonly CroudiaProvider _provider;
        private readonly TimelineTypes _type;
        private IDisposable _disposable;
        private long _lastId;

        public ObservableCollection<StatusViewModel> Statuses { get; }

        public StatusTimelineViewModel(ILocalization localization, INavigationService navigationService, CroudiaProvider provider, TimelineTypes type)
        : base(localization, navigationService)
        {
            Title = GetLocalizedString(type.GetTitle());
            Icon = GetLocalizedString(type.GetIcon());
            NavigationTitle = Title;
            Statuses = new ObservableCollection<StatusViewModel>();
            _provider = provider;
            _type = type;
            if (_type == TimelineTypes.Public)
            {
                // If first load this VM, OnTabNavigatedTo does not call.
                OnTabNavigatedTo();
            }
        }

        public override void OnTabNavigatedFrom()
        {
            _disposable?.Dispose();
        }

        public override void OnTabNavigatedTo()
        {
            _disposable = ConnectTimeline().Subscribe(w =>
            {
                Statuses.Insert(0, new StatusViewModel(Localization, NavigationService, w));
                _lastId = w.Id;
            });
        }

        private IObservable<Status> ConnectTimeline()
        {
            switch (_type)
            {
                case TimelineTypes.Public:
                    return _provider.Croudia.Statuses.GetPublicTimelineAsObservable(since_id => _lastId);

                case TimelineTypes.Home:
                    return _provider.Croudia.Statuses.GetHomeTimelineAsObservable(since_id => _lastId);

                case TimelineTypes.Mentions:
                    return _provider.Croudia.Statuses.GetMentionsAsObservable(since_id => _lastId);
            }
            throw new InvalidOperationException();
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