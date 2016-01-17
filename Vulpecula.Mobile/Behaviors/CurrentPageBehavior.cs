using System;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.ViewModels.Primitives;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Behaviors
{
    public class CurrentPageBehavior : Behavior<TabbedPage>
    {
        private Page _currentPage;

        /// <param name="bindable">To be added.</param>
        /// <summary>
        /// Attaches to the superclass and then calls the <see cref="M:Xamarin.Forms.Behavior`1.OnAttachedTo(T)" /> method on this object.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAttachedTo(TabbedPage bindable)
        {
            bindable.CurrentPageChanged += BindableOnCurrentPageChanged;
            _currentPage = bindable.CurrentPage;
            base.OnAttachedTo(bindable);
        }

        /// <param name="bindable">To be added.</param>
        /// <summary>
        /// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(T)" /> method and then detaches from the superclass.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnDetachingFrom(TabbedPage bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.CurrentPageChanged -= BindableOnCurrentPageChanged;
        }

        private void BindableOnCurrentPageChanged(object sender, EventArgs eventArgs)
        {
            if (_currentPage != null)
            {
                (_currentPage.BindingContext as TabbedViewModel)?.OnTabNavigatedFrom();
                _currentPage = null;
            }
            var tabbedPage = sender as TabbedPage;
            var context = tabbedPage?.CurrentPage; // CurrentPage is NavgationBar?
            if (context == null)
                return;
            NavigationService.ConfigureCurrent(context);
            _currentPage = ((NavigationPage) context).CurrentPage;
            (_currentPage.BindingContext as TabbedViewModel)?.OnTabNavigatedTo();
        }
    }
}