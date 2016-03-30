using System;
using System.Diagnostics;
using System.Linq;

using Microsoft.Practices.ObjectBuilder2;

using Prism.Windows.Navigation;

using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.Services;
using Vulpecula.Universal.Services.Primitive;
using Vulpecula.Universal.Services.Tags;
using Vulpecula.Universal.ViewModels.Timelines.Primitives;

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class TimelineViewModel : TimelineViewModelBase
    {
        private readonly Func<SuspendableService, TimelineType, long, bool> _cond =
        (w, t, i) => ((TimelineTag) w.Tag).Id == i && ((TimelineTag) w.Tag).Type == t;

        public TimelineViewModel(Column column, CroudiaAccount provider, INavigationService navigationService) : base(provider, navigationService)
        {
            if (column.Type == TimelineType.DirectMessages || column.Type == TimelineType.DirectMessagesAll)
            {
                if (ServiceProvider.SuspendableServices.Any(w => _cond(w, column.Type, provider.User.Id)))
                {
                    var s = ServiceProvider.SuspendableServices.Single(w => _cond(w, column.Type, provider.User.Id)) as DirectMessageTimelineService;
                    s?.Subscribers.Add(AddTimeline);
                    s?.StoredItems.ForEach(AddTimeline);
                    Debug.WriteLine("Aattached to exist service.");
                }
                else
                {
                    var service = new DirectMessageTimelineService(provider);
                    service.Subscribers.Add(AddTimeline);
                    service.Tag = new TimelineTag { Type = column.Type, Id = provider.User.Id };
                    ServiceProvider.RegisterService(service);
                }
            }
            else
            {
                if (ServiceProvider.SuspendableServices.Any(w => _cond(w, column.Type, provider.User.Id)))
                {
                    var s = ServiceProvider.SuspendableServices.Single(w => _cond(w, column.Type, provider.User.Id)) as StatusTimelineService;
                    s?.Subscribers.Add(AddTimeline);
                    s?.StoredItems.ForEach(AddTimeline);
                    Debug.WriteLine("Aattached to exist service.");
                }
                else
                {
                    var service = new StatusTimelineService(provider, column.Type);
                    service.Subscribers.Add(AddTimeline);
                    service.Tag = new TimelineTag { Type = column.Type, Id = provider.User.Id };
                    ServiceProvider.RegisterService(service);
                }
            }
        }
    }
}