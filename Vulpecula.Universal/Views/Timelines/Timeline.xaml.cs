using Windows.UI.Xaml.Controls;

using Vulpecula.Universal.ViewModels.Timelines;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Vulpecula.Universal.Views.Timelines
{
    public sealed partial class Timeline : UserControl
    {
        public TimelineViewModel ViewModel => DataContext as TimelineViewModel;

        public Timeline()
        {
            InitializeComponent();
        }
    }
}