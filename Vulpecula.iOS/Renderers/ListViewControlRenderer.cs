using System;
using Xamarin.Forms;
using Vulpecula.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(ListView), typeof(ListViewControlRenderer))]

namespace Vulpecula.iOS.Renderers
{
    public class ListViewControlRenderer : ListViewRenderer
    {
        public ListViewControlRenderer()
        {
            this.ElementChanged += (sender, e) =>
            {
                var element = this.Element as ListView;
                if (element == null)
                {
                    return;
                }
                element.Footer = new ContentView();
            };
        }
    }
}

