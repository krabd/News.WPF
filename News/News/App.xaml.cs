using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using News.Utils.Extensions;
using News.ViewModels;

namespace News
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                DispatcherUnhandledException += ProcessUnhandledException;

                var serviceCollection = new ServiceCollection();
                var startupService = new StartupService();

                startupService.Configure(serviceCollection);
                var serviceProvider = startupService.BuildProvider(serviceCollection);

                var main = serviceProvider.Resolve<MainWindowViewModel>();
                var window = new MainWindow {DataContext = main};
                Current.MainWindow = window;
                window.Show();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                Environment.Exit(1);
            }

            base.OnStartup(e);
        }

        private void ProcessUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Debug.WriteLine($"UnhandledException: {e.Exception.Message}. Trace: {e.Exception.StackTrace}");
            e.Handled = true;
        }
    }
}
