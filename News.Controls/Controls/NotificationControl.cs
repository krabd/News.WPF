using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using News.Utils;

namespace News.Controls.Controls
{
    public class NotificationControl : Control
    {
        public const string POPUP_NAME = "PART_MainPopup";

        private readonly Timer _timer;

        private Popup _popup;

        public static readonly DependencyProperty CloseNotificationSecondsProperty = DependencyProperty.Register(
            nameof(CloseNotificationSeconds), typeof(int), typeof(NotificationControl), new PropertyMetadata(3));

        public int CloseNotificationSeconds
        {
            get => (int) GetValue(CloseNotificationSecondsProperty);
            set => SetValue(CloseNotificationSecondsProperty, value);
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            nameof(Message), typeof(string), typeof(NotificationControl), new PropertyMetadata(default(string)));

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public NotificationControl()
        {
            _timer = new Timer(OnCloseDelay, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Open()
        {
            _timer.Change(TimeSpan.FromSeconds(CloseNotificationSeconds), TimeSpan.FromSeconds(CloseNotificationSeconds));

            Tools.DispatchedInvoke(() =>
            {
                _popup.IsOpen = true;
            });
        }

        public void Close()
        {
            Tools.DispatchedInvoke(() =>
            {
                _popup.IsOpen = false;
            });
        }

        private void OnCloseDelay(object sender)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);

            Close();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _popup = (Popup)GetTemplateChild(POPUP_NAME);
        }
    }
}
