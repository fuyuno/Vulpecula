using System.Collections.ObjectModel;

using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.CroudiaObjects;

namespace Vulpecula.Universal.ViewModels.Contents
{
    public class StatusesTimelineViewModel : TimelineViewModel
    {
        public ObservableCollection<Status> Statuses { get; set; }

        public StatusesTimelineViewModel(Timeline timeline) : base(timeline)
        {
            this.Statuses = new ObservableCollection<Status>();
        }
    }
}