using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Vulpecula.Universal.BgTask
{
    public static class ToastNotificationWrapper
    {
        public static ToastNotification PopToast(string title, string content, NotificationSounds sound)
        {
            ToastNotificationManager.History.Clear();

            var notifySound = sound.ToString();
            if (notifySound.Contains("Call") || notifySound.Contains("Alarm"))
            {
                notifySound = "Looping." + notifySound;
            }
            var payload =
                $@"<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>{title}</text>
            <text>{content}</text>
        </binding>
    </visual>
    <audio src='ms-winsoundevent:Notification.{notifySound}' />
</toast>";
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(payload);

            var notification = new ToastNotification(xmlDoc);
            ToastNotificationManager.CreateToastNotifier().Show(notification);
            return notification;
        }
    }
}