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
            get { return (string) GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public EmptyListBehavior()
        {
            _count = 0;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            var source = AssociatedObject.ItemsSource as INotifyCollectionChanged;
            var items = AssociatedObject.Items;
            if (items != null)
            {
                _count = items.Count;
                Action();
            }

            if (source != null)
            {
                _disposable = source.ToObservable().Subscribe(w =>
                {
                    switch (w.EventArgs.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            _count++;
                            break;

                        case NotifyCollectionChangedAction.Remove:
                            _count--;
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
        }

        protected override void OnUnloaded()
        {
            _disposable?.Dispose();
            base.OnUnloaded();
        }

        private void Action()
        {
            var frameworkElement = (FrameworkElement) ((FrameworkElement) AssociatedObject.Parent).FindName(Target);
            if (frameworkElement != null)
            {
                if (_count > 0)
                {
                    Visibility = Visibility.Visible;
                    frameworkElement.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Visibility = Visibility.Collapsed;
                    frameworkElement.Visibility = Visibility.Visible;
                }
            }
        }
    }
}