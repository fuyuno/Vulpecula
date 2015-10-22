using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Vulpecula.Universal.Converters
{
    internal class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var b = value as bool?;
            if (!b.HasValue)
                return Visibility.Collapsed;
            return b.Value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}