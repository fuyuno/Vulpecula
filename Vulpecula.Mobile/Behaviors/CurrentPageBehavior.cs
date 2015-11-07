using System;

using Vulpecula.Mobile.ViewModels.Timelines.Primitives;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Behaviors
{
    public class CurrentPageBehavior : Behavior<TabbedPage>
    {
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
            var tabbedPage = sender as TabbedPage;
            var context = tabbedPage?.CurrentPage.BindingContext;
            if (!(context is TabbedViewModel))
                return;

            var viewModel = (TabbedViewModel)context;
            tabbedPage.Title = viewModel.NavigationTitle;
        }
    }
}