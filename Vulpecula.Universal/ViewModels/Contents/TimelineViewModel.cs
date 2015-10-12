using Prism.Mvvm;

using Vulpecula.Universal.Models;

namespace Vulpecula.Universal.ViewModels.Contents
{
    public class TimelineViewModel : BindableBase
    {
        public Timeline TimelineSetting { get; private set; }

        public TimelineViewModel(Timeline timeline)
        {
            this.TimelineSetting = timeline;
        }
    }
}