using System.Reflection;

using Android.App;
using Android.OS;

using JetBrains.Annotations;

using Vulpecula.Mobile;
using Vulpecula.Mobile.Models;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Vulpecula.Android
{
    [UsedImplicitly]
    [Activity(Label = "Vulpecula.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            var asm = Assembly.GetExecutingAssembly();
            this.LoadApplication(new App(new ModelLocator("Vulpecula.Android.Models", asm.FullName)));
        }
    }
}