using System;
using System.Threading.Tasks;

using Windows.UI.Popups;

namespace Vulpecula.Universal.Models.Dialogs
{
    public static class MessageDialogWrapper
    {
        /// <summary>
        /// ダイアログを表示します。
        /// </summary>
        public static async Task ShowOkMessageDialogAsync(string content, string title = "")
        {
            var dialog = new MessageDialog(content, title);
            dialog.Commands.Add(new UICommand("OK"));
            await dialog.ShowAsync();
        }

        /// <summary>
        /// 「OK」「キャンセル」を選択できるダイアログを表示します。
        /// </summary>
        public static async Task<DialogCommands> ShowOkCalcelMessageDialogAsync(string content, string title = "")
        {
            var dialog = new MessageDialog(content, title);
            dialog.Commands.Add(new UICommand("OK") { Id = DialogCommands.Ok });
            dialog.Commands.Add(new UICommand("キャンセル") { Id = DialogCommands.Cancel });

            var result = await dialog.ShowAsync();
            return (DialogCommands)result.Id;
        }

        /// <summary>
        /// 「はい」「いいえ」を選択できるダイアログを表示します。
        /// </summary>
        public static async Task<DialogCommands> ShowYesNoMessageDialogAsync(string content, string title = "")
        {
            var dialog = new MessageDialog(content, title);
            dialog.Commands.Add(new UICommand("はい") { Id = DialogCommands.Yes });
            dialog.Commands.Add(new UICommand("いいえ") { Id = DialogCommands.No });

            var result = await dialog.ShowAsync();
            return (DialogCommands)result.Id;
        }

        /// <summary>
        /// 「はい」「いいえ」「キャンセル」を選択できるダイアログを表示します。
        /// </summary>
        public static async Task<DialogCommands> ShowYesNoCancelMessageDialogAsync(string content, string title = "")
        {
            var dialog = new MessageDialog(content, title);
            dialog.Commands.Add(new UICommand("はい") { Id = DialogCommands.Yes });
            dialog.Commands.Add(new UICommand("いいえ") { Id = DialogCommands.No });
            dialog.Commands.Add(new UICommand("キャンセル") { Id = DialogCommands.Cancel });

            var result = await dialog.ShowAsync();
            return (DialogCommands)result.Id;
        }
    }
}