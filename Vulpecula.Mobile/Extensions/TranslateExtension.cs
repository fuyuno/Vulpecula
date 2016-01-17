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
            _localization = App.ModelLocator.GetModel<ILocalization>();
        }

        #region IMarkupExtension implementation

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(Text))
                return "";
            return _localization.GetString(Text);
        }

        #endregion
    }
}