using Vulpecula.Universal.ViewModels.Timelines.Primitives;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Vulpecula.Universal.Views.Timelines.Primitive
{
    public sealed partial class StatusView : UserControl
    {
        public StatusViewModel ViewModel => this.DataContext as StatusViewModel;

        public StatusView()
        {
            this.InitializeComponent();
            this.DataContextChanged += (sender, args) => this.ViewModel?.Initialize();
        }
    }
}