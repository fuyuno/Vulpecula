using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Vulpecula.Universal.Extensions;
using Vulpecula.Universal.Services;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Vulpecula.Universal
{
    /// <summary>
    ///     それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public AppShell()
        {
            InitializeComponent();
            Unloaded += (sender, args) => ServiceProvider.SuspendService();

            //
            MenuView.DataContextChanged += MenuViewDataContextChanged;
            TweetArea.DataContextChanged += TweetAreaDataContextChanged;
        }

        private void MenuViewDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            MenuView.DataContextChanged -= MenuViewDataContextChanged;

            MenuView.ViewModel.Subscribe(nameof(MenuView.ViewModel.EventFired), e =>
            {
                if (!MenuView.ViewModel.EventFired)
                    return;
                RootSplitView.IsPaneOpen = false;
                MenuView.ViewModel.EventFired = false;
            });
            MenuView.ViewModel.Subscribe(nameof(MenuView.ViewModel.IsTweetPaneOpen), e =>
            {
                // Do not format this line....
                TweetArea.Visibility = MenuView.ViewModel.IsTweetPaneOpen ? Visibility.Visible : Visibility.Collapsed;
            });
        }

        private void TweetAreaDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            TweetArea.DataContextChanged -= TweetAreaDataContextChanged;

            TweetArea.ViewModel.Subscribe(nameof(TweetArea.ViewModel.IsPublishedCloseRequest), e1 =>
            {
                if (!TweetArea.ViewModel.IsPublishedCloseRequest)
                    return;
                TweetArea.Visibility = Visibility.Collapsed;
                TweetArea.ViewModel.IsPublishedCloseRequest = false;
                MenuView.ViewModel.IsTweetPaneOpen = false;
            });
        }

        public void SetRootFrame(Frame frame)
        {
            if (RootSplitView != null)
                RootSplitView.Content = frame;
            else
                Content = frame;
        }
    }
}