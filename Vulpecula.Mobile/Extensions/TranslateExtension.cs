using System;

using Vulpecula.Mobile.Models.Interfaces;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vulpecula.Mobile.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private readonly ILocalization _localization;

        public string Text { get; set; }

        public TranslateExtension()
        {
            this._localization = App.ModelLocator.GetModel<ILocalization>();
        }

        #region IMarkupExtension implementation

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                return "";
            }
            return this._localization.GetString(this.Text);
        }

        #endregion
    }
}