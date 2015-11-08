using Vulpecula.Mobile.Models;

using Xamarin.Forms;

namespace Vulpecula.Mobile
{
    public class App : Application
    {
        public static ModelLocator ModelLocator { get; private set; }

        public App(ModelLocator locator)
        {
            ModelLocator = locator;
            var bs = new Bootstrapper();
            bs.Run(this);
        }
    }
}