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
            if (this.Command?.Path == null || sender.BindingContext == null)
            {
                return;
            }

            // Binding 情報を解析して、Element.BidingContext から Command と CommandParameter を取得する
            var bindingContext = sender.BindingContext;
            if (string.IsNullOrWhiteSpace(this.Command.Path))
            {
                this._command = bindingContext as ICommand;
            }
            else
            {
                var value = (from p in bindingContext.GetType().GetPropertiesHierarchical()
                    where p.CanRead && this.Command.Path.Equals(p.Name)
                    select p.GetValue(bindingContext)).FirstOrDefault();
                if (this.Command.Converter != null)
                {
                    value = this.Command.Converter.Convert(
                        value,
                        typeof (ICommand),
                        this.Command.ConverterParameter,
                        CultureInfo.CurrentCulture);
                }
                this._command = value as ICommand;
            }
            if (string.IsNullOrWhiteSpace(CommandParameter?.Path))
            {
                this._commandParameter = bindingContext;
            }
            else
            {
                var value = (from p in bindingContext.GetType().GetPropertiesHierarchical()
                    where p.CanRead && this.CommandParameter.Path.Equals(p.Name)
                    select p.GetValue(bindingContext)).FirstOrDefault();
                if (this.CommandParameter.Converter != null)
                {
                    value = this.CommandParameter.Converter.Convert(
                        value,
                        typeof (object),
                        this.CommandParameter.ConverterParameter,
                        CultureInfo.CurrentCulture);
                }
                this._commandParameter = value;
            }

            // 実行可能であれば Command を呼び出す
            if (this._command == null || !this._command.CanExecute(this._commandParameter))
            {
                return;
            }

            this._command.Execute(this._commandParameter);
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