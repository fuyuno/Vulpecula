using System;
using System.Collections.ObjectModel;
using System.Linq;

using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

using Microsoft.Practices.ObjectBuilder2;

using Prism.Windows.Navigation;

using Vulpecula.Models.Base;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Timelines.Primitive;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Timelines.Primitives
{
    public class TimelineViewModelBase : ViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly CroudiaProvider _provider;

        public ObservableCollection<StatusViewModel> Statuses { get; }

        protected TimelineViewModelBase(CroudiaProvider provider, INavigationService navigationService)
        {
            Statuses = new ObservableCollection<StatusViewModel>();
            _provider = provider;
            _navigationService = navigationService;
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

        protected async void AddTimeline(StatusBase status)
        {
            var vm = new StatusViewModel(new StatusModel(status), _provider, _navigationService);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Statuses.Insert(0, vm));
        }
    }
}