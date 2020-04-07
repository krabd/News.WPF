using System.ComponentModel;
using System.Windows;
using News.CoreModule.Enums;
using News.CoreModule.Interfaces;
using News.CoreModule.Views;
using News.Utils;

namespace News.CoreModule.Services
{
    public class WindowService : IWindowService
    {
        private readonly Window _parentWindow;

        public WindowService(Window parentWindow)
        {
            _parentWindow = parentWindow;
        }

        public void Show(INotifyPropertyChanged dataContext, string title)
        {
            var window = new BaseWindow
            {
                Width = 1000,
                Height = 800,
                DataContext = dataContext,
                Owner = _parentWindow ?? Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = title
            };

            Tools.DispatchedInvoke(() => window.Show(), false, _parentWindow?.Dispatcher);
        }

        public bool? ShowDialog(INotifyPropertyChanged dataContext, string title)
        {
            var window = new BaseWindow
            {
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,
                DataContext = dataContext,
                Owner = _parentWindow ?? Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = title
            };

            return Tools.DispatchedInvoke(() => window.ShowDialog(), false, _parentWindow?.Dispatcher);
        }

        public bool? ShowMessage(string message, string title, NotificationResult resultType)
        {
            var dialogResult = Tools.DispatchedInvoke(() =>
            {
                var result = MessageBox.Show(message, title, GetButtonType(resultType));

                return result == MessageBoxResult.Yes || result == MessageBoxResult.OK
                    ? true
                    : result == MessageBoxResult.No
                        ? false
                        : (bool?)null;
            }, false, _parentWindow?.Dispatcher);

            var window = _parentWindow;
            while (window?.Owner != null)
            {
                window = window.Owner;
            }
            window?.Activate();

            return dialogResult;

            MessageBoxButton GetButtonType(NotificationResult notificationResult)
            {
                var buttons = MessageBoxButton.OK;

                switch (notificationResult)
                {
                    case NotificationResult.Yes:
                        buttons = MessageBoxButton.OK;
                        break;

                    case NotificationResult.YesNo:
                        buttons = MessageBoxButton.YesNo;
                        break;

                    case NotificationResult.YesNoCancel:
                        buttons = MessageBoxButton.YesNoCancel;
                        break;
                }

                return buttons;
            }
        }
    }
}