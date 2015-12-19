using System.Collections.Generic;
using System.Linq;

using UIKit;

using Vulpecula.iOS.Renderers;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof (ContentPage), typeof (NavigationPageRenderer))]

namespace Vulpecula.iOS.Renderers
{
    public class NavigationPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var bindItems = (this.Element as ContentPage)?.ToolbarItems.OrderBy(w => w.Priority);
            var items = this.NavigationController.TopViewController.NavigationItem;
            var rightItems = new List<UIBarButtonItem>();
            var leftItems = items.LeftBarButtonItems?.ToList() ?? new List<UIBarButtonItem>();
            foreach (var item in bindItems)
            {
                if (item.Priority < 0)
                {
                    leftItems.Add(item.ToUIBarButtonItem());
                }
                else
                {
                    rightItems.Add(item.ToUIBarButtonItem());
                }
            }

            items.SetRightBarButtonItems(rightItems.ToArray(), animated);
            items.SetLeftBarButtonItems(leftItems.ToArray(), animated);
        }
    }
}