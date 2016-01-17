using Vulpecula.iOS.Renderers;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof (ListView), typeof (ListViewControlRenderer))]

namespace Vulpecula.iOS.Renderers
{
    public class ListViewControlRenderer : ListViewRenderer
    {
        public ListViewControlRenderer()
        {
            ElementChanged += (sender, e) =>
            {
                var element = Element;
                if (element == null)
                    return;
                element.Footer = new ContentView();
            };
        }
    }
}