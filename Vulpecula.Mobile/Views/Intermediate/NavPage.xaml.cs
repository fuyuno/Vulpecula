using Vulpecula.Mobile.ViewModels.Primitives;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Views.Intermediate
{
    public partial class NavPage : NavigationPage
    {
        public NavPage(Page page) : base(page)
        {
            InitializeComponent();
            Title = page.Title;
            Icon = page.Icon;

            page.BindingContextChanged += (sender, args) =>
            {
                var viewModel = page.BindingContext as TabbedViewModel;
                if (viewModel == null)
                    return;
                Title = viewModel.Title;
                Icon = viewModel.Icon;
            };
        }
    }
}