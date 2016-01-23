using System.Linq;

using Windows.ApplicationModel.Background;

using Vulpecula.Universal.BgTask;

namespace Vulpecula.Universal.Models.Notifications
{
    public static class NotificationRegistry
    {
        public static void Initialize()
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
                task.Value.Unregister(true);

            if (BackgroundTaskRegistration.AllTasks.All(w => w.Value.Name != nameof(QuickReplyBackgroundTask)))
            {
                var builder = new BackgroundTaskBuilder
                {
                    Name = nameof(QuickReplyBackgroundTask),
                    TaskEntryPoint = typeof (QuickReplyBackgroundTask).FullName
                };
                builder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
                builder.SetTrigger(new ToastNotificationActionTrigger());
                builder.Register();
            }
        }
    }
}