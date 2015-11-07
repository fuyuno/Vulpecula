using System.Reflection;

using JetBrains.Annotations;

using UIKit;

using Vulpecula.Mobile;
using Vulpecula.Mobile.Models;

namespace Vulpecula.iOS
{
    [UsedImplicitly]
    public class Application
    {
        // This is the main entry point of the application.
        private static void Main(string[] args)
        {
            var asm = Assembly.GetExecutingAssembly();
            MobileCross.ModelLocator = new ModelLocator("Vulpecula.iOS.Models", asm.FullName);

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}