using Vulpecula.Universal.Models.Timelines;

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class ColumnViewModel
    {
        private readonly Column _column;

        public string Name => this._column.Info.Name;

        public string Icon => this._column.User.ProfileImageUrlHttps;

        public ColumnViewModel(Column column)
        {
            this._column = column;
        }
    }
}