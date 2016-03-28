using Windows.UI.Xaml.Controls;

using Vulpecula.Universal.ViewModels.Pages;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Vulpecula.Universal.Views.Pages
{
    /// <summary>
    ///     それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class AccountPage : Page
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        public AccountPageViewModel ViewModel => DataContext as AccountPageViewModel;
    }
}