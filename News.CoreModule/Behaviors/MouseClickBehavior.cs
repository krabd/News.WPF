using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace News.CoreModule.Behaviors
{
    public class MouseClickBehavior : Behavior<FrameworkElement>
    {
        private FrameworkElement _parent;
        private Point _mouseStartPosition;
        private bool _isMouseDownNoMove;

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command), typeof(ICommand), typeof(MouseClickBehavior), new PropertyMetadata(default(ICommand)));

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            nameof(CommandParameter), typeof(object), typeof(MouseClickBehavior), new PropertyMetadata(default(object)));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
            AssociatedObject.MouseMove += OnMouseMove;

            _parent = (FrameworkElement)AssociatedObject.Parent;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDownNoMove = true;

            Debug.WriteLine("OnMouseLeftButtonDown");

            _mouseStartPosition = e.GetPosition(_parent);
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("OnMouseLeftButtonUp");

            if (_isMouseDownNoMove && Command != null && Command.CanExecute(CommandParameter))
                Command.Execute(CommandParameter);

            _isMouseDownNoMove = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMouseDownNoMove) return;

            var diff = e.GetPosition(_parent) - _mouseStartPosition;
            if (diff.X > 3 || diff.Y > 3)
            {
                Debug.WriteLine("Cancel OnMouseMove");

                _isMouseDownNoMove = false;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
            AssociatedObject.MouseMove -= OnMouseMove;
        }
    }
}
