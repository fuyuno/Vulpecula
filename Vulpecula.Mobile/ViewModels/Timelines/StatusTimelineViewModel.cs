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
    public class StatusTimelineViewModel : TabbedViewModel
    {
        private readonly CroudiaProvider _provider;
        private readonly TimelineTypes _type;
        private IDisposable _disposable;
        private long _lastId;

        public ObservableCollection<StatusViewModel> Statuses { get; }

        public StatusTimelineViewModel(ILocalization localization, INavigationService navigationService, CroudiaProvider provider, TimelineTypes type)
            : base(localization, navigationService)
        {
            Title = this.GetLocalizedString(type.GetTitle());
            Icon = GetLocalizedString(type.GetIcon());
            NavigationTitle = this.Title;
            this.Statuses = new ObservableCollection<StatusViewModel>();
            this._provider = provider;
            this._type = type;
            if (this._type == TimelineTypes.Public)
            {
                // If first load this VM, OnTabNavigatedTo does not call.
                this._disposable = this._provider.Croudia.Statuses.GetPublicTimelineAsObservable()
                    .Subscribe(w =>
                    {
                        this.Statuses.Insert(0, new StatusViewModel(this.Localization, this.NavigationService, w));
                        this._lastId = w.Id;
                    });
            }
        }

        public override void OnTabNavigatedFrom()
        {
            this._disposable?.Dispose();
        }

        public override void OnTabNavigatedTo()
        {
            this._disposable = this.ConnectTimeline().Subscribe(w =>
            {
                this.Statuses.Insert(0, new StatusViewModel(this.Localization, this.NavigationService, w));
                this._lastId = w.Id;
            });
        }

        private IObservable<Status> ConnectTimeline()
        {
            switch (this._type)
            {
                case TimelineTypes.Public:
                    return this._provider.Croudia.Statuses.GetPublicTimelineAsObservable(since_id => this._lastId);

                case TimelineTypes.Home:
                    return this._provider.Croudia.Statuses.GetHomeTimelineAsObservable(since_id => this._lastId);

                case TimelineTypes.Mentions:
                    return this._provider.Croudia.Statuses.GetMentionsAsObservable(since_id => this._lastId);
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