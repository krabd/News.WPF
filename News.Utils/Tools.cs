using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace News.Utils
{
    public static class Tools
    {
        public static void DispatchedInvoke(Action action, bool useUiThread = true, Dispatcher dispatcher = null)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (dispatcher == null && Application.Current?.Dispatcher == null)
                return;

            dispatcher = dispatcher ?? Application.Current.Dispatcher;

            if (!useUiThread || dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                try
                {
                    if (!dispatcher.HasShutdownStarted)
                        dispatcher.Invoke(action);
                }
                catch (OperationCanceledException)
                {
                    // порождается при закрытии приложения
                }
            }
        }

        public static TResult DispatchedInvoke<TResult>(Func<TResult> action, bool useUiThread = true, Dispatcher dispatcher = null)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (dispatcher == null && Application.Current?.Dispatcher == null)
                return default;

            dispatcher = dispatcher ?? Application.Current.Dispatcher;

            if (!useUiThread || dispatcher.CheckAccess())
                return action();

            try
            {
                return !dispatcher.HasShutdownStarted ? dispatcher.Invoke(action) : default;
            }
            catch (OperationCanceledException)
            {
                // порождается при закрытии приложения
                return default;
            }
        }

        public static async Task WhenAll(params Task[] tasks)
        {
            await Task.WhenAll(tasks.Where(t => t != null)).ConfigureAwait(false);
        }

        public static async Task WhenAll(IEnumerable<Task> tasks, params Task[] additionalTasks)
        {
            await Task.WhenAll(tasks.Concat(additionalTasks).Where(t => t != null)).ConfigureAwait(false);
        }
    }
}