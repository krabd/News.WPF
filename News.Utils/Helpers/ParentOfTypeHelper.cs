using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace News.Utils.Helpers
{
    public static class ParentOfTypeHelper
    {
        private static DependencyObject GetVisualParent(DependencyObject element)
        {
            var parent = VisualTreeHelper.GetParent(element);
            if (parent == null && element is FrameworkElement frameworkElement)
                parent = frameworkElement.Parent;

            return parent;
        }

        private static IEnumerable<DependencyObject> GetVisualParents(DependencyObject element)
        {
            while (true)
            {
                var parent = GetVisualParent(element);
                var dependencyObject = parent;
                element = parent;

                if (dependencyObject == null)
                    break;

                yield return element;
            }
        }

        public static T VisualParentOfType<T>(DependencyObject element) where T : DependencyObject
        {
            if (element != null)
                return GetVisualParents(element).OfType<T>().DefaultIfEmpty().First();

            throw new ArgumentNullException("Нельзя искать родителя несуществующего элемента" + nameof(element));
        }
    }
}
