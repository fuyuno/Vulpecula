using System.Diagnostics;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WinRTXamlToolkit.Interactivity;

namespace Vulpecula.Universal.Behaviors
{
    // SettingsFlyout is deprecated.
    public class SettingsFlyoutOpenBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// 操作対象 Flyout
        /// </summary>
        public static readonly DependencyProperty FlyoutProperty =
            DependencyProperty.Register(nameof(Flyout), typeof (SettingsFlyout), typeof (SettingsFlyoutOpenBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty FlyoutDataContextProperty =
            DependencyProperty.Register(nameof(FlyoutDataContext), typeof (object), typeof (SettingsFlyoutOpenBehavior), new PropertyMetadata(null, FlyoutDataContextChanged));

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof (bool), typeof (SettingsFlyoutOpenBehavior), new PropertyMetadata(false, IsOpenChanged));

        public SettingsFlyout Flyout
        {
            get { return (SettingsFlyout)this.GetValue(FlyoutProperty); }
            set { this.SetValue(FlyoutProperty, value); }
        }

        public object FlyoutDataContext
        {
            get { return this.GetValue(FlyoutDataContextProperty); }
            set { this.SetValue(FlyoutDataContextProperty, value); }
        }

        public bool IsOpen
        {
            get { return (bool)this.GetValue(IsOpenProperty); }
            set { this.SetValue(IsOpenProperty, value); }
        }

        private static void FlyoutDataContextChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var behavior = dependencyObject as SettingsFlyoutOpenBehavior;
            if (behavior == null)
                return;
            behavior.Flyout.DataContext = dependencyPropertyChangedEventArgs.NewValue;
        }

        private static void IsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as SettingsFlyoutOpenBehavior;
            if (behavior == null)
                return;

            if ((bool)e.NewValue)
            {
                Debug.WriteLine("Shown");
                behavior.Flyout.ShowIndependent();
            }
            else
            {
                behavior.Flyout.Hide();
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            Flyout.Loaded += FlyoutOnLoaded;
            Flyout.Unloaded += FlyoutOnUnloaded;
        }

        protected override void OnDetaching()
        {
            Flyout.Loaded -= FlyoutOnLoaded;
            Flyout.Unloaded -= FlyoutOnUnloaded;
            base.OnDetaching();
        }

        private void FlyoutOnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            IsOpen = false;
        }

        private void FlyoutOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            IsOpen = true;
        }
    }
}