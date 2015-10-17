using Prism.Windows.Mvvm;

using Vulpecula.Universal.Models.Timelines;

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class TimelineViewModelBase : ViewModelBase
    {
        public ColumnInfo ColumnInfo { get; set; }

        protected TimelineViewModelBase(ColumnInfo columnInfo)
        {
            this.ColumnInfo = columnInfo;
        }
    }
}