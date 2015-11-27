using Microsoft.Practices.Unity;

using Prism.Mvvm;
using Prism.Navigation;
using Prism.Unity;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.ViewModels;
using Vulpecula.Mobile.Views;

using Xamarin.Forms;

namespace Vulpecula.Mobile
{
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Creates the root <see cref="T:Xamarin.Forms.Page" /> for the application.
        /// </summary>
        /// <returns>
        /// The <see cref="T:Xamarin.Forms.Page" />
        /// </returns>
        protected override Page CreateMainPage()
        {
            return NavigationService.RootPage;
        }

        /// <summary>
        /// Used to register types with the container that will be used by your application.
        /// </summary>
        protected override void RegisterTypes()
        {
            // AccountManager モデルをシングルトンで↓
            Container.RegisterType<Configuration>(new ContainerControlledLifetimeManager());
            Container.RegisterType<AccountManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            NavigationService.Configure(new MainPage());
        }

        /// <summary>
        /// Configures the <see cref="T:Prism.Mvvm.ViewModelLocator" /> used by Prism.
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory(type => Container.Resolve(type));
        }

        protected override void InitializeMainPage()
        {
            var main = NavigationService.RootPage as MainPage;
            var viewModel = main?.BindingContext;
            ((MainPageViewModel)viewModel)?.Initialize();
        }
    }
}