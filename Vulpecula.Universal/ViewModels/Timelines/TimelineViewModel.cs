using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

using Microsoft.Practices.ObjectBuilder2;

using Prism.Windows.Navigation;

using Vulpecula.Models.Base;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Services;
using Vulpecula.Universal.Models.Services.Primitive;
using Vulpecula.Universal.Models.Services.Tags;
using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.Models.Timelines.Primitive;
using Vulpecula.Universal.ViewModels.Primitives;
using Vulpecula.Universal.ViewModels.Timelines.Primitives;

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class TimelineViewModel : ViewModel
    {
        private readonly Func<SuspendableService, TimelineType, long, bool> _cond =
        (w, t, i) => ((TimelineTag) w.Tag).Id == i && ((TimelineTag) w.Tag).Type == t;

        private readonly INavigationService _navigationService;
        private readonly CroudiaProvider _provider;

        public ObservableCollection<StatusViewModel> Statuses { get; }

        public TimelineViewModel(Column column, CroudiaProvider provider, INavigationService navigationService)
        {
            Statuses = new ObservableCollection<StatusViewModel>();
            var column1 = column;
            _provider = provider;
            _navigationService = navigationService;

            if (column1.Type == TimelineType.DirectMessages || column1.Type == TimelineType.DirectMessagesAll)
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
                    service.Tag = new TimelineTag { Type = column1.Type, Id = provider.User.Id };
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
                    var service = new StatusTimelineService(provider, column1.Type);
                    service.Subscribers.Add(AddTimeline);
                    service.Tag = new TimelineTag { Type = column1.Type, Id = provider.User.Id };
                    ServiceProvider.RegisterService(service);
                }
            }
        }

        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems.Cast<StatusViewModel>())
            {
                var i = Statuses.First(w => w.Model.Id == item.Model.Id);
                i.IsExpanded = !i.IsExpanded;
                Statuses.Where(w => w.Model.Id != item.Model.Id).ForEach(w => w.IsExpanded = false);
            }
            var listView = sender as ListView;
            if (listView != null)
                listView.SelectedIndex = -1;
        }

        private async void AddTimeline(StatusBase status)
        {
            var vm = new StatusViewModel(new StatusModel(status), _provider, _navigationService);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Statuses.Insert(0, vm));
        }
    }
}