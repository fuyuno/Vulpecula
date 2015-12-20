using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

using Microsoft.Practices.ObjectBuilder2;

using Vulpecula.Models;
using Vulpecula.Models.Base;
using Vulpecula.Streaming;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Notifications;
using Vulpecula.Universal.Models.Services;
using Vulpecula.Universal.Models.Services.Primitive;
using Vulpecula.Universal.Models.Services.Tags;
using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.Models.Timelines.Primitive;
using Vulpecula.Universal.ViewModels.Primitives;
using Vulpecula.Universal.ViewModels.Timelines.Primitives;

// ReSharper disable PossibleMultipleEnumeration

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class ColumnViewModel : ViewModel
    {
        private readonly Func<SuspendableService, TimelineType, long, bool> _cond = (w, t, i) => ((TimelineTag)w.Tag).Id == i && ((TimelineTag)w.Tag).Type == t;
        private readonly CroudiaProvider _provider;
        private readonly User _user;

        private int _counter = -100;
        public Column Column { get; }

        public string Icon => _user.ProfileImageUrlHttps;
        public ObservableCollection<StatusViewModel> Statuses { get; }

        private ColumnViewModel(Column column, CroudiaProvider provider)
        {
            Column = column;
            _provider = provider;
            _user = provider.User;
            Statuses = new ObservableCollection<StatusViewModel>();

            if (Column.Type == TimelineType.DirectMessages || Column.Type == TimelineType.DirectMessagesAll)
            {
                if (ServiceProvider.SuspendableServices.Any(w => _cond(w, column.Type, provider.User.Id)))
                {
                    var s = ServiceProvider.SuspendableServices.Single(w => _cond(w, column.Type, provider.User.Id)) as DirectMessageTimelineService;
                    s?.Subscribers.Add(AddTimeline);
                    Debug.WriteLine("Aattached to exist service.");
                }
                else
                {
                    var service = new DirectMessageTimelineService(provider);
                    service.Subscribers.Add(AddTimeline);
                    service.Tag = new TimelineTag { Type = Column.Type, Id = provider.User.Id };
                    ServiceProvider.RegisterService(service);
                }
            }
            else
            {
                if (ServiceProvider.SuspendableServices.Any(w => _cond(w, column.Type, provider.User.Id)))
                {
                    var s = ServiceProvider.SuspendableServices.Single(w => _cond(w, column.Type, provider.User.Id)) as StatusTimelineService;
                    s?.Subscribers.Add(AddTimeline);
                    Debug.WriteLine("Aattached to exist service.");
                }
                else
                {
                    var service = new StatusTimelineService(provider, Column.Type);
                    service.Subscribers.Add(AddTimeline);
                    service.Tag = new TimelineTag { Type = Column.Type, Id = provider.User.Id };
                    ServiceProvider.RegisterService(service);
                }
            }

            CompositeDisposable.Add(Observable.FromEventPattern<NotifyCollectionChangedEventArgs>(Statuses, "CollectionChanged")
                .Throttle(TimeSpan.FromSeconds(CroudiaStreaming.TimeSpan.Seconds * 0.3))
                .Subscribe(w =>
                {
                    if (_counter > 0)
                    {
                        switch (Column.Type)
                        {
                            case TimelineType.Home:
                            case TimelineType.Public:
                            case TimelineType.PublicAll:
                            case TimelineType.User:
                                // case TimelineType.Favorite:
                                if (this.Column.EnableNotity)
                                {
                                    ToastNotificationWrapper.PopToast($"新着通知 ({this.Column.Name})", $"{_counter}件の新しいささやきがあります。");
                                }
                                break;

                            case TimelineType.Mentions:
                            case TimelineType.MentionsAll:
                                if (!this.Column.EnableNotity)
                                {
                                    break;
                                }
                                if (w.EventArgs.NewItems.Count > 1)
                                {
                                    ToastNotificationWrapper.PopToast($"新着返信通知 ({this.Column.Name})", $"{_counter}件の新しい返信があります。");
                                }
                                else
                                {
                                    var status = (StatusViewModel)w.EventArgs.NewItems[0];
                                    ToastNotificationWrapper.PopQuickReplyToast($"新着返信通知 ({this.Column.Name})", status.Model, status.User.User, NotificationSounds.Mail);
                                }
                                break;

                            case TimelineType.DirectMessages:
                            case TimelineType.DirectMessagesAll:
                                if (this.Column.EnableNotity)
                                {
                                    ToastNotificationWrapper.PopToast($"新着通知 ({this.Column.Name})", $"{_counter}件の新しいメールがあります。", NotificationSounds.SMS);
                                }
                                break;

                            case TimelineType.Event:
                            case TimelineType.EventAll:
                                if (this.Column.EnableNotity)
                                {
                                    ToastNotificationWrapper.PopToast($"新着通知 ({this.Column.Name})", $"{_counter}件の新しいイベントがあります。", NotificationSounds.Mail);
                                }
                                break;
                        }
                    }
                    _counter = 0;
                }));
        }

        public static ColumnViewModel Create(Column column)
        {
            if (AccountManager.Instance.Providers.All(w => w.User.Id != column.UserId))
                throw new KeyNotFoundException($"UserId:{column.UserId} is not found in users that loading.");
            return new ColumnViewModel(column, AccountManager.Instance.Providers.Single(w => w.User.Id == column.UserId));
        }

        private async void AddTimeline(StatusBase status)
        {
            var vm = new StatusViewModel(new StatusModel(status), _provider);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Statuses.Insert(0, vm));
            _counter++;
        }

        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems.Cast<StatusViewModel>())
            {
                this.Statuses.First(w => w.Model.Id == item.Model.Id).IsExpanded = true;
                this.Statuses.Where(w => w.Model.Id != item.Model.Id).ForEach(w => w.IsExpanded = false);
            }
        }
    }
}