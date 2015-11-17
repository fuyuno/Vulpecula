using System.Linq;
using System.Threading.Tasks;

using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;

using JetBrains.Annotations;

// ReSharper disable PossibleMultipleEnumeration

namespace Vulpecula.Universal.BgTask
{
    [UsedImplicitly]
    public sealed class QuickReplyBackgroundTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;
        private string ConsumerKey => "b101ef32b1fd6c3e11b33f3ae4f1e91c358c02f6dd98b650e19b74edfa61d69c";

        private string ConsumerSecret => "4bcd6e665dd41de886c90fb77a3c9430049a33693d88398ff72d6c618e62e540";

        // Toast の Arguments に、 AccessToken を渡すのはアリ？ナシ？
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            var toastDetails = taskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;
            if (toastDetails == null)
            {
                ToastNotificationWrapper.PopToast("Error", "toastDetails is not ToasNotificationActionTriggerDetail!", NotificationSounds.Default);
                return;
            }

            var arguments = toastDetails.Argument;
            var userInput = toastDetails.UserInput;

            await this.SendWhisper(userInput, arguments);

            _deferral.Complete();
        }

        private async Task SendWhisper(ValueSet valueSet, string arguments)
        {
            var args = QueryString.Parse(arguments);
            var text = valueSet["status"].ToString();
            var inReply = args.Single(w => w.Key == "in_reply_to_status_id").Value;
            var inReplySn = args.Single(w => w.Key == "in_reply_to_screen_name").Value;
            var accessToken = args.Single(w => w.Key == "access_token").Value;

            var croudia = new Croudia(ConsumerKey, ConsumerSecret) { AccessToken = accessToken };

            // ReSharper disable once InconsistentNaming
            await croudia.Statuses.UpdateAsync(status => $"@{inReplySn} {text}", in_reply_to_status_id => inReply);
            ToastNotificationWrapper.PopToast("Sent!", $"Sent reply to @{inReplySn}!", NotificationSounds.Default);
        }
    }
}