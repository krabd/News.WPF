using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace News.CoreModule.Behaviors
{
    public class DragBehavior : Behavior<FrameworkElement>
    {
        private readonly TranslateTransform _transform = new TranslateTransform();
        private FrameworkElement _parent;
        private Point _mouseStartPosition;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
            AssociatedObject.MouseMove += OnMouseMove;

            _parent = (FrameworkElement)AssociatedObject.Parent;
            AssociatedObject.RenderTransform = _transform;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mouseStartPosition = e.GetPosition(_parent);
            _mouseStartPosition.X -= _transform.X;
            _mouseStartPosition.Y -= _transform.Y;

            AssociatedObject.CaptureMouse();
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var diff = e.GetPosition(_parent) - _mouseStartPosition;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _transform.X = diff.X;
                _transform.Y = diff.Y;
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