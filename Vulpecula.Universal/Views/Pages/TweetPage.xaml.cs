using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Vulpecula.Universal.ViewModels.Pages;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Vulpecula.Universal.Views.Pages
{
    public sealed partial class TweetPage : Page
    {
        private readonly InputPane _inputPane;
        private double? _rootHeight;

        public TweetPage()
        {
            InitializeComponent();
            _inputPane = InputPane.GetForCurrentView();
            _rootHeight = null;
        }

        public TweetPageViewModel ViewModel => DataContext as TweetPageViewModel;

        private void OnKeyboardShowing(InputPane sender, InputPaneVisibilityEventArgs args)
        {
            _rootHeight = !_rootHeight.HasValue ? Root.ActualHeight : _rootHeight;
            var height = Root.ActualHeight;
            if (height < _rootHeight)
            {
                // BUG: OnKeyboardHiding does not called, "Root" size is smaller than Keyboard height.
                //    : Fix control size temporary.
                height = _rootHeight.Value;
            }
            Root.Height = height - sender.OccludedRect.Height + 48 /* CommandBar */;
            Root.VerticalAlignment = VerticalAlignment.Top;
        }

        private void OnKeyboardHiding(InputPane sender, InputPaneVisibilityEventArgs args)
        {
            if (!_rootHeight.HasValue)
                return;
            Root.Height = _rootHeight.Value;
            Root.VerticalAlignment = VerticalAlignment.Stretch;
        }

        #region Overrides of Page

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _inputPane.Hiding += OnKeyboardHiding;
            _inputPane.Showing += OnKeyboardShowing;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _inputPane.Hiding -= OnKeyboardHiding;
            _inputPane.Showing -= OnKeyboardShowing;
        }

        #endregion Overrides of Page
    }
}