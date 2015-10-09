using Vulpecula.Models;

using Status = Vulpecula.Universal.Models.CroudiaObjects.Status;

namespace Vulpecula.Universal.ViewModels.Contents
{
    public class StatusTimelineViewModel : TimelineViewModel<Status>
    {
        public StatusTimelineViewModel(User user) : base(user)
        {
        }
    }
}