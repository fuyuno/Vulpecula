using System;
using System.Collections.Specialized;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Vulpecula.Universal.Extensions;

using WinRTXamlToolkit.Interactivity;

namespace Vulpecula.Universal.Behaviors
{
    /// <summary>
    /// ItemsSource が空の時、同一ネスト以下にある Target に指定されたコントロールを表示します。
    /// </summary>
    public class EmptyListBehavior : Behavior<ListView>
    {
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register(
                nameof(Target),
                typeof (string),
                typeof (EmptyListBehavior),
                new PropertyMetadata(string.Empty));

        private int _count;
        private IDisposable _disposable;

        public string Target
        {
            get { return (string)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public EmptyListBehavior()
        {
            this._count = 0;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            var source = this.AssociatedObject.ItemsSource as INotifyCollectionChanged;
            if (source != null)
            {
                this._disposable = source.ToObservable().Subscribe(w =>
                {
                    switch (w.EventArgs.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            this._count++;
                            break;

                        case NotifyCollectionChangedAction.Remove:
                            this._count--;
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
        }

        protected override void OnUnloaded()
        {
            this._disposable.Dispose();
            base.OnUnloaded();
        }

        private void Action()
        {
            var frameworkElement = (FrameworkElement)((FrameworkElement)this.AssociatedObject.Parent).FindName(this.Target);
            if (frameworkElement != null)
            {
                if (this._count > 0)
                {
                    this.Visibility = Visibility.Visible;
                    frameworkElement.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.Visibility = Visibility.Collapsed;
                    frameworkElement.Visibility = Visibility.Visible;
                }
            }
        }
    }
}