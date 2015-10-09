using Vulpecula.Models;

namespace Vulpecula.Universal.ViewModels.Contents
{
    public class MessageTimelineViewModel : TimelineViewModel<SecretMail>
    {
        public MessageTimelineViewModel(User user) : base(user)
        {
        }
    }
}