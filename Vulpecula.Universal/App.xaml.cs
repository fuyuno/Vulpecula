using System.Threading.Tasks;

using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Microsoft.ApplicationInsights;
using Microsoft.Practices.Unity;

using Prism.Logging;
using Prism.Unity.Windows;
using Prism.Windows.AppModel;

using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Notifications;
using Vulpecula.Universal.Services;

namespace Vulpecula.Universal
{
    /// <summary>
    ///     既定の Application クラスを補完するアプリケーション固有の動作を提供します。
    /// </summary>
    public sealed partial class App : PrismUnityApplication
    {
        /// <summary>
        ///     単一アプリケーション オブジェクトを初期化します。これは、実行される作成したコードの
        ///     最初の行であるため、main() または WinMain() と論理的に等価です。
        /// </summary>
        public App()
        {
            WindowsAppInitializer.InitializeAsync(WindowsCollectors.Metadata |
                                                  WindowsCollectors.Session);
            InitializeComponent();

            Resuming += OnResuming;
            UnhandledException += (sender, args) => { };
        }

        /// <summary>
        ///     Override this method with the initialization logic of your application. Here you can initialize services,
        ///     repositories, and so on.
        /// </summary>
        /// <param name="args">
        ///     The <see cref="T:Windows.ApplicationModel.Activation.IActivatedEventArgs" /> instance containing the
        ///     event data.
        /// </param>
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            NotificationRegistry.Initialize();

            // Prism.Unity
            Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()),
                                                        new ContainerControlledLifetimeManager());

            // Singleton
            Container.RegisterType<Configuration>(new ContainerControlledLifetimeManager());
            Container.RegisterType<AccountManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ColumnManager>(new ContainerControlledLifetimeManager());

            return base.OnInitializeAsync(args);
        }

        /// <summary>
        ///     Override this method with logic that will be performed after the application is initialized. For example,
        ///     navigating to the application's home page.
        /// </summary>
        /// <param name="args">
        ///     The <see cref="T:Windows.ApplicationModel.Activation.LaunchActivatedEventArgs" /> instance
        ///     containing the event data.
        /// </param>
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate("Main", null);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Creates the shell of the app.
        /// </summary>
        /// <param name="rootFrame" />
        /// <returns>
        ///     The shell of the app.
        /// </returns>
        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = Container.Resolve<AppShell>();
            shell.SetRootFrame(rootFrame);
            return shell;
        }

        /// <summary>
        ///     Invoked when the application is suspending, but before the general suspension calls.
        /// </summary>
        /// <returns>
        ///     Task to complete.
        /// </returns>
        protected override Task OnSuspendingApplicationAsync()
        {
            ServiceProvider.SuspendService();
            return base.OnSuspendingApplicationAsync();
        }

        private async void OnResuming(object sender, object o)
        {
            await ServiceProvider.StartService();
        }

        // おちる
        // http://blog.okazuki.jp/entry/2015/10/24/114618
        /// <summary>
        ///     Create the <see cref="T:Prism.Logging.ILoggerFacade" /> used by the bootstrapper.
        /// </summary>
        /// <remarks>
        ///     The base implementation returns a new DebugLogger.
        /// </remarks>
        protected override ILoggerFacade CreateLogger()
        {
            return new EmptyLogger();
        }
    }
}