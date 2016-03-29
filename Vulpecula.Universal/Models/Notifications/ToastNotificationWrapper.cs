using System.Collections.Generic;

using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

using Vulpecula.Universal.BgTask;
using Vulpecula.Universal.Models.Timelines.Primitive;

namespace Vulpecula.Universal.Models.Notifications
{
    public static class ToastNotificationWrapper
    {
        public static ToastNotification PopToast(string title, string content, NotificationSounds sound = NotificationSounds.Default)
        {
            ToastNotificationManager.History.Clear();

            var notifySound = sound.ToString();
            if (notifySound.Contains("Call") || notifySound.Contains("Alarm"))
                notifySound = "Looping." + notifySound;
            var payload =
            $@"
<toast>
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
            ToastNotificationManager.CreateToastNotifier()
                                    .Show(notification);
            return notification;
        }

        // Vulpecula.Models.User がほしい
        public static ToastNotification PopQuickReplyToast(string title, StatusModel status, CroudiaAccount account, NotificationSounds sound = NotificationSounds.Default)
        {
            ToastNotificationManager.History.Clear();

            var notifySound = sound.ToString();
            if (notifySound.Contains("Call") || notifySound.Contains("Alarm"))
                notifySound = "Looping." + notifySound;
            var arguments = new[]
            {
                new KeyValuePair<string, object>("access_token", account.Croudia.AccessToken),
                new KeyValuePair<string, object>("in_reply_to_status_id", status.Id),
                new KeyValuePair<string, object>("in_reply_to_screen_name", status.User.ScreenName)
            };
            var payload =
            $@"
<toast activationType='background' launch='args'>
    <visual>
        <binding template='ToastGeneric'>
            <image placement='appLogoOverride' src='{account.User.ProfileImageUrlHttps}' />
            <text>{title}</text>
            <text>{status.Text}</text>
        </binding>
    </visual>
    <actions>
        <input id='status'
               type='text'
               title='Reply to @{account.User.ScreenName}'
               placeHolderContent='Hello!' />
        <action activationType='background'
                arguments='{QueryString.Query(arguments)}'
                content='Whisper' />
        <action activationType='system'
                arguments='dismiss'
                content='Dismiss' />
    </actions>
    <audio src='ms-winsoundevent:Notification.{notifySound}' />
</toast>";
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(payload);

            var notification = new ToastNotification(xmlDoc);
            ToastNotificationManager.CreateToastNotifier()
                                    .Show(notification);
            return notification;
        }
    }
}