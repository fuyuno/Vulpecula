using Vulpecula.Universal.ViewModels;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace Vulpecula.Universal.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContextChanged += (sender, e) => this.ViewModel = this.DataContext as MainPageViewModel;
        }

        // http://sourcechord.hatenablog.com/entry/2015/08/27/135425
        // 一部 Event類で、 Event="{Binding HogeCommand}" すると、
        // error WMC9999「オブジェクト参照がオブジェクト インスタンスに設定されていません。」
        // が出るので、 Event="{x:Bind ViewModel.HogeCommand}" で逃げる
        public MainPageViewModel ViewModel { get; set; }
    }
}