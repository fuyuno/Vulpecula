using Windows.UI.Xaml.Controls;

using Vulpecula.Universal.Extensions;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Services;
using Vulpecula.Universal.ViewModels;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Vulpecula.Universal
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public AppShell()
        {
            InitializeComponent();
            Loaded += async (sender, args) =>
            {
                AccountManager.Instance.ResetAccounts();
                ColumnManager.Instance.ClearColumns();

                await AccountManager.Instance.InitializeAccounts();
                await ColumnManager.Instance.InitializeColumns();
            };
            Unloaded += (sender, args) => ServiceProvider.SuspendService();
            MenuView.DataContextChanged += (sender, args) =>
            {
                var viewModel = (MenuViewModel) sender.DataContext;
                viewModel.Subscribe(nameof(viewModel.EventFired), e =>
                {
                    if (viewModel.EventFired)
                    {
                        RootSplitView.IsPaneOpen = false;
                        viewModel.EventFired = false;
                    }
                });
            };
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