using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace News.Behaviors
{
    public class ListViewScrollToBottomDetectBehavior : Behavior<ListView>
    {
        private ScrollViewer _scrollViewer;

        public static readonly DependencyProperty EndOfScrollCommandProperty = DependencyProperty.Register(
            nameof(EndOfScrollCommand), typeof(ICommand), typeof(ListViewScrollToBottomDetectBehavior), new PropertyMetadata(default(ICommand)));

        public ICommand EndOfScrollCommand
        {
            get => (ICommand) GetValue(EndOfScrollCommandProperty);
            set => SetValue(EndOfScrollCommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += OnListViewLoaded;
        }

        private void OnListViewLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= OnListViewLoaded;

            var border = (Border)VisualTreeHelper.GetChild(AssociatedObject, 0);
            _scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
            _scrollViewer.ScrollChanged += OnScrollChanged;
        }

        private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (_scrollViewer.VerticalOffset != _scrollViewer.ScrollableHeight) return;

            if (EndOfScrollCommand != null && EndOfScrollCommand.CanExecute(AssociatedObject.Items.Count))
                EndOfScrollCommand.Execute(AssociatedObject.Items.Count);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            _scrollViewer.ScrollChanged -= OnScrollChanged;
        }
    }
}
