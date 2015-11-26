using System;

using JetBrains.Annotations;

using Prism.Navigation;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Models
{
    /// <summary>
    /// Modified Class.
    /// http://matatabi-ux.hateblo.jp/entry/2014/11/07/120000
    /// https://www.mallibone.com/post/xamarin.forms-navigation-with-mvvm-light
    /// </summary>
    [UsedImplicitly]
    public class NavigationService : INavigationService
    {
        public static Page RootPage { get; private set; }

        /// <summary>
        /// Navigates to the most recent entry in the back navigation history by popping the calling Page off the navigation stack.
        /// </summary>
        /// <param name="useModalNavigation">If <c>true</c> uses PopModalAsync, if <c>false</c> uses PopAsync</param>
        /// <param name="animated">If <c>true</c> the transition is animated, if <c>false</c> there is no animation on transition.</param>
        public void GoBack(bool useModalNavigation = true, bool animated = true)
        {
            Pop(useModalNavigation, animated);
        }

        /// <summary>
        /// Initiates navigation to the target specified by the <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type which will be used to identify the name of the navigation target.</typeparam>
        /// <param name="parameters">The navigation parameters</param>
        /// <param name="useModalNavigation">If <c>true</c> uses PopModalAsync, if <c>false</c> uses PopAsync</param>
        /// <param name="animated">If <c>true</c> the transition is animated, if <c>false</c> there is no animation on transition.</param>
        public void Navigate<T>(NavigationParameters parameters = null, bool useModalNavigation = true, bool animated = true)
        {
            var page = Activator.CreateInstance(typeof (T)) as Page;
            if (page == null)
                throw new InvalidCastException("T cannot cast to Xamarin.Forms.Page.");
            Push(page, useModalNavigation, animated);
            PrepareNavigation(page, parameters);
        }

        /// <summary>
        /// Initiates navigation to the target specified by the <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the target to navigate to.</param>
        /// <param name="parameters">The navigation parameters</param>
        /// <param name="useModalNavigation">If <c>true</c> uses PopModalAsync, if <c>false</c> uses PopAsync</param>
        /// <param name="animated">If <c>true</c> the transition is animated, if <c>false</c> there is no animation on transition.</param>
        public void Navigate(string name, NavigationParameters parameters = null, bool useModalNavigation = true, bool animated = true)
        {
            var type = Type.GetType(name);
            var page = Activator.CreateInstance(type) as Page;
            if (page == null)
                throw new InvalidCastException("T cannot cast to Xamarin.Forms.Page.");
            Push(page, useModalNavigation, animated);
            PrepareNavigation(page, parameters);
        }

        public async void GoHome(bool animated = true)
        {
            await RootPage.Navigation.PopToRootAsync(animated);
        }

        public static void Configure(Page rootPage)
        {
            RootPage = rootPage;
        }

        private static async void Push(Page page, bool useModalNavigation, bool animated)
        {
            var navigation = RootPage.Navigation;
            if (useModalNavigation)
            {
                await navigation.PushModalAsync(new NavigationPage(page), animated);
            }
            else
            {
                await navigation.PushAsync(page, animated);
            }
        }

        private static async void Pop(bool useModalNavigation, bool animated)
        {
            var navigation = RootPage.Navigation;
            if (useModalNavigation)
            {
                await navigation.PopModalAsync(animated);
            }
            else
            {
                await navigation.PopAsync(animated);
            }
        }

        private static void PrepareNavigation(Page page, NavigationParameters parameters)
        {
            var navigationAware = page.BindingContext as INavigationAware;
            navigationAware?.OnNavigatedTo(parameters);

            EventHandler onDisappearing = null;
            onDisappearing = (sender, e) =>
            {
                page.Disappearing -= onDisappearing;
                navigationAware?.OnNavigatedFrom(null);
            };
            page.Disappearing += onDisappearing;
        }
    }
}