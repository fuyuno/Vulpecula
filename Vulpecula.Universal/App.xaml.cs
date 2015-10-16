using System.Threading.Tasks;

using Windows.ApplicationModel.Activation;

using Microsoft.ApplicationInsights;

using Prism.Unity.Windows;

using Vulpecula.Universal.Models;

namespace Vulpecula.Universal
{
    /// <summary>
    /// 既定の Application クラスを補完するアプリケーション固有の動作を提供します。
    /// </summary>
    sealed partial class App : PrismUnityApplication
    {
        #region Statics

        public static readonly VulpeculaSettings AppSettings = new VulpeculaSettings();

        #endregion

        /// <summary>
        /// 単一アプリケーション オブジェクトを初期化します。これは、実行される作成したコードの
        /// 最初の行であるため、main() または WinMain() と論理的に等価です。
        /// </summary>
        public App()
        {
            WindowsAppInitializer.InitializeAsync(
                WindowsCollectors.Metadata |
                WindowsCollectors.Session);
            this.InitializeComponent();
            AppSettings.Initialize();
        }

        /// <summary>
        /// Override this method with logic that will be performed after the application is initialized. For example, navigating to the application's home page.
        /// </summary>
        /// <param name="args">The <see cref="T:Windows.ApplicationModel.Activation.LaunchActivatedEventArgs" /> instance containing the event data.</param>
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            this.NavigationService.Navigate("Main", null);
            return Task.CompletedTask;
        }
    }
}