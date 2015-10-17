using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Vulpecula.Universal.ViewModels.Timelines;

namespace Vulpecula.Universal.Selectors
{
    public class TimelineTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StatusesDataTemplate { get; set; }

        public DataTemplate MessagesDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is StatusesTimelineViewModel)
                return this.StatusesDataTemplate;

            if (item is MessagesTimelineViewModel)
                return this.MessagesDataTemplate;

            return base.SelectTemplateCore(item);
        }
    }
}