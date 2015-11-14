using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Windows.UI.Core;

using Vulpecula.Models;
using Vulpecula.Models.Base;
using Vulpecula.Universal.Models;
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
        private readonly Column _column;

        private readonly Func<SuspendableService, TimelineType, long, bool> _cond = (w, t, i) => ((TimelineTag)w.Tag).Id == i && ((TimelineTag)w.Tag).Type == t;
        private readonly CroudiaProvider _provider;
        private readonly User _user;

        public string Name => _column.Name;

        public string Icon => _user.ProfileImageUrlHttps;
        public ObservableCollection<StatusViewModel> Statuses { get; }

        private ColumnViewModel(Column column, CroudiaProvider provider)
        {
            _column = column;
            _provider = provider;
            _user = provider.User;
            Statuses = new ObservableCollection<StatusViewModel>();

            if (_column.Type == TimelineType.DirectMessages || _column.Type == TimelineType.DirectMessagesAll)
            {
                if (ServiceProvider.SuspendableServices.Any(w => _cond(w, column.Type, provider.User.Id)))
                {
                    var s = ServiceProvider.SuspendableServices.Single(w => _cond(w, column.Type, provider.User.Id)) as DirectMessageTimelineService;
                    s?.Subscribers.Add(AddTimeline);
                }
                else
                {
                    var service = new DirectMessageTimelineService(provider);
                    service.Subscribers.Add(AddTimeline);
                    service.Tag = new TimelineTag { Type = _column.Type, Id = provider.User.Id };
                    ServiceProvider.RegisterService(service);
                }
            }
            else
            {
                if (ServiceProvider.SuspendableServices.Any(w => _cond(w, column.Type, provider.User.Id)))
                {
                    var s = ServiceProvider.SuspendableServices.Single(w => _cond(w, column.Type, provider.User.Id)) as StatusTimelineService;
                    s?.Subscribers.Add(AddTimeline);
                }
                else
                {
                    var service = new StatusTimelineService(provider, _column.Type);
                    service.Subscribers.Add(AddTimeline);
                    service.Tag = new TimelineTag { Type = _column.Type, Id = provider.User.Id };
                    ServiceProvider.RegisterService(service);
                }
            }
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
        }
    }
}