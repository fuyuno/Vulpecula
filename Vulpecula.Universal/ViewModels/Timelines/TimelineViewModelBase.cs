using System.Collections.ObjectModel;

using Prism.Windows.Mvvm;

using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class TimelineViewModelBase : ViewModelBase
    {
        public Column ColumnInfo { get; set; }

        public ObservableCollection<ViewModel> Statuses { get; }

        protected TimelineViewModelBase(Column columnInfo)
        {
            this.ColumnInfo = columnInfo;
            this.Statuses = new ObservableCollection<ViewModel>();
        }
    }
}