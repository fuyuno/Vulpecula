using System.Collections.ObjectModel;

using Vulpecula.Models;
using Vulpecula.Universal.Models.Timelines;

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class MessagesTimelineViewModel : TimelineViewModelBase
    {
        public ObservableCollection<SecretMail> SecretMails { get; set; }

        public MessagesTimelineViewModel(ColumnInfo columnInfo) : base(columnInfo)
        {
            this.SecretMails = new ObservableCollection<SecretMail>();
        }
    }
}