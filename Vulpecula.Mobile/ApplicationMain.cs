using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Views;

using Xamarin.Forms;

namespace Vulpecula.Mobile
{
    public class ApplicationMain : Application
    {
        public static ModelLocator ModelLocator { get; private set; }

        public ApplicationMain(ModelLocator locator)
        {
            ModelLocator = locator;
            this.MainPage = new RootPage();
        }
    }
}