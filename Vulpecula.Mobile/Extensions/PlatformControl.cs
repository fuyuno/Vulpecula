using Xamarin.Forms;

namespace Vulpecula.Mobile.Extensions
{
    public class PlatformControl
    {
        // iOS

        public static bool GetiOS(BindableObject obj)
        {
            return (bool)obj.GetValue(iOSProperty);
        }

        public static void SetiOS(BindableObject obj, bool value)
        {
            obj.SetValue(iOSProperty, value);
        }

        public static readonly BindableProperty iOSProperty = 
            BindableProperty.CreateAttached("iOS", typeof(bool), typeof(PlatformControl), true);


        // Android

        public static bool GetAndroid(BindableObject obj)
        {
            return (bool)obj.GetValue(AndroidProperty);
        }

        public static void SetAndroid(BindableObject obj, bool value)
        {
            obj.SetValue(AndroidProperty, value);
        }

        public static readonly BindableProperty AndroidProperty =
            BindableProperty.CreateAttached("Android", typeof(bool), typeof(PlatformControl), true);

    }
}

