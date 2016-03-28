using Windows.UI.Xaml.Controls;

using Vulpecula.Universal.ViewModels.Pages;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Vulpecula.Universal.Views.Pages
{
    public sealed partial class MiniTweetPage : UserControl
    {
        public MiniTweetPage()
        {
            InitializeComponent();
        }

        public MiniTweetPageViewModel ViewModel => DataContext as MiniTweetPageViewModel;
    }
}