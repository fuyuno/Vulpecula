using Windows.UI.Xaml.Controls;

using Vulpecula.Universal.ViewModels.Pages;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Vulpecula.Universal.Views.Pages
{
    public sealed partial class TweetPage : Page
    {
        public TweetPage()
        {
            InitializeComponent();
        }

        public TweetPageViewModel ViewModel => DataContext as TweetPageViewModel;
    }
}