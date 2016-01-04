using System;
using System.Collections.Specialized;

using Vulpecula.Mobile.Extensions;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Behaviors
{
    public class EmptyListBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty TargetProperty =
            BindableProperty.Create(nameof(Target), typeof(string), typeof(EmptyListBehavior), string.Empty);

        public static readonly BindableProperty IsReverseProperty =
            BindableProperty.Create(nameof(IsReverse), typeof(bool), typeof(EmptyListBehavior), false);

        private ListView _associatedObject;

        private int _count;
        private IDisposable _disposable;

        public string Target
        {
            get { return (string)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public bool IsReverse
        {
            get { return (bool)GetValue(IsReverseProperty); }
            set { SetValue(IsReverseProperty, value); }
        }

        public EmptyListBehavior()
        {
            this._count = 0;
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            this._associatedObject = bindable;
            this._associatedObject.Opacity = 0.0;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            this._disposable.Dispose();
            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            var items = this._associatedObject.ItemsSource as INotifyCollectionChanged;
            if (items == null)
            {
                return;
            }
            this._disposable = items.ToObservable().Subscribe(w =>
                {
                    switch (w.EventArgs.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            this._count += w.EventArgs.NewItems.Count;
                            break;

                        case NotifyCollectionChangedAction.Remove:
                            this._count -= w.EventArgs.OldItems.Count;
                            break;

                        case NotifyCollectionChangedAction.Replace:
                        case NotifyCollectionChangedAction.Move:
                            break;

                        case NotifyCollectionChangedAction.Reset:
                            this._count = 0;
                            break;
                    }
                    this.Action();
                });
        }

        private void Action()
        {
            var element = this._associatedObject.ParentView.FindByName<VisualElement>(this.Target);
            if (element != null)
            {
                if (this._count > 0)
                {
                    this._associatedObject.Opacity = this.IsReverse ? 0.0 : 1.0;
                    element.Opacity = this.IsReverse ? 1.0 : 0.0;
                }
                else
                {
                    this._associatedObject.Opacity = this.IsReverse ? 1.0 : 0.0;
                    element.Opacity = this.IsReverse ? 0.0 : 1.0;
                }
            }
        }
    }
}