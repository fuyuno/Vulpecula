using Windows.UI.Xaml.Controls;

using WinRTXamlToolkit.Interactivity;

namespace Vulpecula.Universal.Behaviors
{
    public class UnSelectableListBoxBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
            base.OnDetaching();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            AssociatedObject.SelectedIndex = -1;
        }
    }
}