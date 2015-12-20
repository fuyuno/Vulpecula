using Windows.UI.Xaml.Controls;

using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Services;

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
            this.InitializeComponent();
            this.Loaded += async (sender, args) =>
            {
                // AccountManager.Instance.ResetAccounts();
                // ColumnManager.Instance.ClearColumns();

                await AccountManager.Instance.InitializeAccounts();
                await ColumnManager.Instance.InitializeColumns();
            };
            this.Unloaded += (sender, args) => ServiceProvider.SuspendService();
        }

        public void SetRootFrame(Frame frame)
        {
            this.RootSplitView.Content = frame;
        }
    }
}