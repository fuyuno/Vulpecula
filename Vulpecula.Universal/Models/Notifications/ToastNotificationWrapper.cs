using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

using Vulpecula.Universal.ViewModels.Timelines.Primitives;

namespace Vulpecula.Universal.Models.Notifications
{
    public static class ToastNotificationWrapper
    {
        public static ToastNotification PopToast(string title, string content, NotificationSounds sound = NotificationSounds.Default)
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

        // Vulpecula.Models.User がほしい
        public static ToastNotification PopQuickReplyToast(string title, string content, UserViewModel user, NotificationSounds sound = NotificationSounds.Default)
        {
            ToastNotificationManager.History.Clear();

            var notifySound = sound.ToString();
            if (notifySound.Contains("Call") || notifySound.Contains("Alarm"))
            {
                notifySound = "Looping." + notifySound;
            }
            var payload =
                $@"<toast activationType='foreground' launch='args'>
    <visual>
        <binding template='ToastGeneric'>
            <image placement='appLogoOverride' src='{user.Icon}' />
            <text>{title}</text>
            <text>{content}</text>
        </binding>
    </visual>
    <actions>
        <input id='status'
               type='text'
               title='Reply to {user.ScreenName}'
               placeHolderContent='Hello!' />
        <action activetionType='foreground'
                arguments='quickReply'
                content='Whisper' />
        <action activationType='foreground'
                arguments='cancel'
                content='Dismiss' />
    </actions>
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