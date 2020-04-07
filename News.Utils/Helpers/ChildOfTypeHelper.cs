using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace News.Utils.Helpers
{
    public static class ChildOfTypeHelper
    {
        public static IEnumerable<T> GetChildrenOfType<T>(DependencyObject element) where T : DependencyObject
        {
            return GetChildren(element).OfType<T>();
        }

        private static IEnumerable<DependencyObject> GetChildren(DependencyObject element)
        {
            var children = LogicalTreeHelper.GetChildren(element);
            foreach (var child in children.OfType<DependencyObject>())
            {
                yield return child;
                foreach (var i in GetChildren(child))
                {
                    yield return i;
                }
            }
        }
    }
}
