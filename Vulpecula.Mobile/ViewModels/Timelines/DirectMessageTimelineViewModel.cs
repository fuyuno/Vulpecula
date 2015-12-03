﻿using System;
using System.Collections.ObjectModel;

using System.Reactive.Linq;

using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.ViewModels.Timelines.Primitives;
using Vulpecula.Streaming.Reactive;

namespace Vulpecula.Mobile.ViewModels.Timelines
{
    public class DirectMessageTimelineViewModel : TabbedViewModel
    {
        public ObservableCollection<DirectMailViewModel> Statuses { get; set; }

        private readonly CroudiaProvider _provider;
        private long _lastId = 0;
        private IDisposable _disposable;

        public DirectMessageTimelineViewModel(ILocalization localization, INavigationService navigationService, CroudiaProvider provider)
            : base(localization, navigationService)
        {
            Title = this.GetLocalizedString("Messages");
            Icon = "message";
            NavigationTitle = this.GetLocalizedString("MessagesTimeline");
            this.Statuses = new ObservableCollection<DirectMailViewModel>();
            this._provider = provider;
        }

        public override void OnTabNavigatedFrom()
        {
            this._disposable?.Dispose();
        }

        public override void OnTabNavigatedTo()
        {
            var obs = this._provider.Croudia.SecretMails.ReceivedAsObservable(since_id => this._lastId);
            obs.Merge(this._provider.Croudia.SecretMails.SentAsObservable(since_id => this._lastId));
            this._disposable = obs.Subscribe(w =>
            {
                this.Statuses.Insert(0, new DirectMailViewModel(this.Localization, this.NavigationService, w));
                this._lastId = w.Id;
            });
        }
    }
}