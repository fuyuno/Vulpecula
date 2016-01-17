using System.Globalization;
using System.Linq;
using System.Windows.Input;

using Microsoft.Practices.Unity.Utility;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Triggers
{
    /// <summary>
    /// Command を実行するトリガーアクション
    /// From: http://matatabi-ux.hateblo.jp/entry/2015/05/01/120000
    /// </summary>
    public class InvokeCommandAction : TriggerAction<Element>
    {
        /// <summary>
        /// トリガーアクションを実行する
        /// </summary>
        /// <param name="sender">アクション実行者</param>
        protected override void Invoke(Element sender)
        {
            if (Command?.Path == null || sender.BindingContext == null)
                return;

            // Binding 情報を解析して、Element.BidingContext から Command と CommandParameter を取得する
            var bindingContext = sender.BindingContext;
            if (string.IsNullOrWhiteSpace(Command.Path))
                _command = bindingContext as ICommand;
            else
            {
                var value = (from p in bindingContext.GetType().GetPropertiesHierarchical()
                             where p.CanRead && Command.Path.Equals(p.Name)
                             select p.GetValue(bindingContext)).FirstOrDefault();
                if (Command.Converter != null)
                {
                    value = Command.Converter.Convert(value,
                                                      typeof (ICommand),
                                                      Command.ConverterParameter,
                                                      CultureInfo.CurrentCulture);
                }
                _command = value as ICommand;
            }
            if (string.IsNullOrWhiteSpace(CommandParameter?.Path))
                _commandParameter = bindingContext;
            else
            {
                var value = (from p in bindingContext.GetType().GetPropertiesHierarchical()
                             where p.CanRead && CommandParameter.Path.Equals(p.Name)
                             select p.GetValue(bindingContext)).FirstOrDefault();
                if (CommandParameter.Converter != null)
                {
                    value = CommandParameter.Converter.Convert(value,
                                                               typeof (object),
                                                               CommandParameter.ConverterParameter,
                                                               CultureInfo.CurrentCulture);
                }
                _commandParameter = value;
            }

            // 実行可能であれば Command を呼び出す
            if (_command == null || !_command.CanExecute(_commandParameter))
                return;

            _command.Execute(_commandParameter);
        }

        #region Command

        /// <summary>
        /// Command
        /// </summary>
        private ICommand _command;

        /// <summary>
        /// Command の Binding 情報
        /// </summary>
        public Binding Command { get; set; }

        #endregion //Command

        #region CommandParameter

        /// <summary>
        /// Command のパラメーター
        /// </summary>
        private object _commandParameter;

        /// <summary>
        /// Command のパラメーターの Binding 情報
        /// </summary>
        public Binding CommandParameter { get; set; }

        #endregion //CommandParameter
    }
}