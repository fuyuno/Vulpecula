using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Universal.Models;

namespace Vulpecula.Universal.ViewModels.Contents
{
    public class MessagesTimelineViewModel : TimelineViewModel
    {
        public ObservableCollection<SecretMail> SecretMails { get; set; }

        public MessagesTimelineViewModel(Timeline timeline) : base(timeline)
        {
            this.SecretMails = new ObservableCollection<SecretMail>();
        }
    }
}