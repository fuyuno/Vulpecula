using System.Collections.ObjectModel;

using Vulpecula.Models;
using Vulpecula.Universal.Models.Timelines;

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class StatusesTimelineViewModel : TimelineViewModelBase
    {
        public ObservableCollection<Status> Statuses { get; set; }

        public StatusesTimelineViewModel(ColumnInfo columnInfo) : base(columnInfo)
        {
            this.Statuses = new ObservableCollection<Status>();
        }
    }
}