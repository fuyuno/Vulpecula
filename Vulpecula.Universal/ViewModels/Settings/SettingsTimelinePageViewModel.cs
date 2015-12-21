using System.Collections.ObjectModel;

using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Settings
{
    public class SettingsTimelinePageViewModel : ViewModel
    {
        public ObservableCollection<Column> TimelineContents { get; }

        public SettingsTimelinePageViewModel()
        {
            this.TimelineContents = new ObservableCollection<Column>();
            foreach (var column in ColumnManager.Instance.Columns)
            {
                this.TimelineContents.Add(column);
            }
        }
    }
}