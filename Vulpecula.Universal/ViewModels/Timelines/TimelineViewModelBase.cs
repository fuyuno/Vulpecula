using Prism.Windows.Mvvm;

using Vulpecula.Universal.Models.Timelines;

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class TimelineViewModelBase : ViewModelBase
    {
        public Column ColumnInfo { get; set; }

        protected TimelineViewModelBase(Column columnInfo)
        {
            this.ColumnInfo = columnInfo;
        }
    }
}