using Windows.UI.Xaml.Controls;

using Vulpecula.Universal.ViewModels;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace Vulpecula.Universal.Views
{
    /// <summary>
    ///     それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // http://sourcechord.hatenablog.com/entry/2015/08/27/135425
        // Blend SDK が存在しない世界なので、
        // が出るので、 Event="{x:Bind ViewModel.HogeCommand}" で逃げる
        public MainPageViewModel ViewModel { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContextChanged += (sender, e) => this.ViewModel = this.DataContext as MainPageViewModel;
        }
    }
}