using System.Collections.ObjectModel;

using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Settings
{
    public class SettingsTimelinePageViewModel : ViewModel
    {
        public SettingsTimelinePageViewModel(ColumnManager columnManager)
        {
            TimelineContents = new ObservableCollection<Column>();
            foreach (var column in columnManager.Columns)
                TimelineContents.Add(column);
        }

        public ObservableCollection<Column> TimelineContents { get; }
    }
}