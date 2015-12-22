using System;
using System.Reactive.Linq;

using Vulpecula.Mobile.Extensions;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Behaviors
{
    /// <summary>
    /// Platform behavior.
    /// </summary>
    public class PlatformBehavior : Behavior<Layout<View>>
    {
        private IDisposable _disposable;
        private bool _flag;
        private Layout<View> _layout;

        protected override void OnAttachedTo(Layout<View> bindable)
        {
            this._layout = bindable;
            this._disposable = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(10))
                .Select(_ => this._flag)
                .DistinctUntilChanged()
                .Where(w => w)
                .Repeat()
                .Subscribe(_ => this.DoAction());
            bindable.ChildAdded += Bindable_ChildAdded;
            bindable.ChildRemoved += Bindable_ChildRemoved;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Layout<View> bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.ChildAdded -= Bindable_ChildAdded;
            bindable.ChildRemoved -= Bindable_ChildRemoved;
            this._disposable.Dispose();
        }

        private void Bindable_ChildRemoved(object sender, ElementEventArgs e)
        {
            //
        }

        private void Bindable_ChildAdded(object sender, ElementEventArgs e)
        {
            this._flag = true;
        }

        private void DoAction()
        {
            var children = this._layout.Children;
            foreach (var child in children)
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    if (!(bool)child.GetValue(PlatformControl.AndroidProperty))
                    {
                        this._layout.Children.Remove(child);
                    }
                }
                if (Device.OS == TargetPlatform.iOS)
                {
                    if (!(bool)child.GetValue(PlatformControl.iOSProperty))
                    {
                        this._layout.Children.Remove(child);
                    }
                }
            }

            this._flag = false;
        }
    }
}