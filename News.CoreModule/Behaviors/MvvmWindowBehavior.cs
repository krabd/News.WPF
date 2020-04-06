using System;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;
using News.CoreModule.Interfaces;
using News.CoreModule.Services;
using News.Utils.Helpers;

namespace News.CoreModule.Behaviors
{
    public class MvvmWindowBehavior : Behavior<FrameworkElement>
    {
        private Window _window;

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject.IsLoaded)
                SetWindowService();
            else
                AssociatedObject.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= OnLoaded;

            SetWindowService();
        }

        private void SetWindowService()
        {
            try
            {
                if (AssociatedObject.DataContext == null) return;

                _window = ParentOfTypeHelper.VisualParentOfType<Window>(AssociatedObject);

                var windowService = AssociatedObject.DataContext.GetType().GetProperties().SingleOrDefault(i => typeof(IWindowService).IsAssignableFrom(i.PropertyType));
                windowService?.SetValue(AssociatedObject.DataContext, new WindowService(_window));
            }
            catch (Exception) { }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Loaded -= OnLoaded;
        }
    }
}
