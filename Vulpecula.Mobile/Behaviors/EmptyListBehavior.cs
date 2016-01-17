using System;
using System.Collections;
using System.Collections.Specialized;

using Vulpecula.Mobile.Extensions;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Behaviors
{
    public class EmptyListBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty TargetProperty = BindableProperty.Create("Target", typeof (string), typeof (EmptyListBehavior), string.Empty);

        public static readonly BindableProperty IsReverseProperty = BindableProperty.Create("IsReverse", typeof (bool), typeof (EmptyListBehavior), false);

        private ListView _associatedObject;

        private int _count;
        private IDisposable _disposable;

        public string Target
        {
            get { return (string) GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public bool IsReverse
        {
            get { return (bool) GetValue(IsReverseProperty); }
            set { SetValue(IsReverseProperty, value); }
        }

        public EmptyListBehavior()
        {
            _count = 0;
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            _associatedObject = bindable;
            _associatedObject.Opacity = 0.0;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            _disposable.Dispose();
            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            var items = _associatedObject.ItemsSource as INotifyCollectionChanged;
            if (items == null)
                return;
            _count = ((IList) _associatedObject.ItemsSource).Count;
            if (_count > 0)
                Action();

            _disposable = items.ToObservable().Subscribe(w =>
            {
                switch (w.EventArgs.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        _count += w.EventArgs.NewItems.Count;
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        _count -= w.EventArgs.OldItems.Count;
                        break;

                    case NotifyCollectionChangedAction.Replace:
                    case NotifyCollectionChangedAction.Move:
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        _count = 0;
                        break;
                }
                Action();
            });
        }

        private void Action()
        {
            var element = _associatedObject.ParentView.FindByName<VisualElement>(Target);
            if (element != null)
            {
                if (_count > 0)
                {
                    _associatedObject.Opacity = IsReverse ? 0.0 : 1.0;
                    element.Opacity = IsReverse ? 1.0 : 0.0;
                }
                else
                {
                    _associatedObject.Opacity = IsReverse ? 1.0 : 0.0;
                    element.Opacity = IsReverse ? 0.0 : 1.0;
                }
            }
        }
    }
}