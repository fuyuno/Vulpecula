using Vulpecula.Droid.Renderers;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentPage), typeof(NavigationPageRenderer))]

namespace Vulpecula.Droid.Renderers
{
    public class NavigationPageRenderer : PageRenderer
    {
        public override void SetWillNotDraw(bool willNotDraw)
        {
            base.SetWillNotDraw(willNotDraw);
        }
    }
}

