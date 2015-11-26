using Windows.UI.Xaml.Controls;

using Vulpecula.Universal.ViewModels;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Vulpecula.Universal.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MenuView : UserControl
    {
        public MenuViewModel ViewModel => this.DataContext as MenuViewModel;

        public MenuView()
        {
            this.InitializeComponent();
        }
    }
}