using Xamarin.Forms;

namespace Vulpecula.Mobile.Views
{
    public partial class RootPage : NavigationPage
    {
        public RootPage() : base(new MainPage())
        {
            InitializeComponent();
        }
    }
}