using System;
using System.Reactive.Linq;

using Vulpecula.Mobile.Extensions;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Behaviors
{
    /// <summary>
    ///     Platform behavior.
    /// </summary>
    public class PlatformBehavior : Behavior<Layout<View>>
    {
        private IDisposable _disposable;
        private bool _flag;
        private Layout<View> _layout;

        protected override void OnAttachedTo(Layout<View> bindable)
        {
            _layout = bindable;
            _disposable = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(10))
                                    .Select(_ => _flag)
                                    .DistinctUntilChanged()
                                    .Where(w => w)
                                    .Repeat()
                                    .Subscribe(_ => DoAction());
            bindable.ChildAdded += Bindable_ChildAdded;
            bindable.ChildRemoved += Bindable_ChildRemoved;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Layout<View> bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.ChildAdded -= Bindable_ChildAdded;
            bindable.ChildRemoved -= Bindable_ChildRemoved;
            _disposable.Dispose();
        }

        private void Bindable_ChildRemoved(object sender, ElementEventArgs e)
        {
            //
        }

        private void Bindable_ChildAdded(object sender, ElementEventArgs e)
        {
            _flag = true;
        }

        private void DoAction()
        {
            var children = _layout.Children;
            foreach (var child in children)
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    if (!(bool) child.GetValue(PlatformControl.AndroidProperty))
                        _layout.Children.Remove(child);
                }
                if (Device.OS == TargetPlatform.iOS)
                {
                    if (!(bool) child.GetValue(PlatformControl.iOSProperty))
                        _layout.Children.Remove(child);
                }
            }

            _flag = false;
        }
    }
}