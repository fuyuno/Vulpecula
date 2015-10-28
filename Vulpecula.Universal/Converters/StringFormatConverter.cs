using System;

using Windows.UI.Xaml.Data;

using Vulpecula.Universal.Helpers;

namespace Vulpecula.Universal.Converters
{
    internal class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.Format(LocalizationHelper.GetStringByFullPath(parameter.ToString()), value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}