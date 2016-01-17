using Xamarin.Forms;

namespace Vulpecula.Mobile.Extensions
{
    public static class PlatformControl
    {
        // ReSharper disable once InconsistentNaming
        public static readonly BindableProperty iOSProperty = BindableProperty.CreateAttached("iOS", typeof (bool), typeof (PlatformControl), true);

        public static readonly BindableProperty AndroidProperty = BindableProperty.CreateAttached("Android", typeof (bool), typeof (PlatformControl), true);

        // iOS

        // ReSharper disable once InconsistentNaming
        public static bool GetiOS(BindableObject obj)
        {
            return (bool) obj.GetValue(iOSProperty);
        }

        // ReSharper disable once InconsistentNaming
        public static void SetiOS(BindableObject obj, bool value)
        {
            obj.SetValue(iOSProperty, value);
        }

        // Android

        public static bool GetAndroid(BindableObject obj)
        {
            return (bool) obj.GetValue(AndroidProperty);
        }

        public static void SetAndroid(BindableObject obj, bool value)
        {
            obj.SetValue(AndroidProperty, value);
        }
    }
}