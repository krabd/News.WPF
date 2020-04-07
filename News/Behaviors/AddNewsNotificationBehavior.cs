using System.Collections.Generic;
using System.Windows;
using System.Windows.Interactivity;
using News.Controls.Controls;
using News.Domain.Models;
using News.Interfaces;
using News.Utils;

namespace News.Behaviors
{
    public class AddNewsNotificationBehavior : Behavior<NotificationControl>
    {
        public static readonly DependencyProperty NotifyAboutNewsProperty = DependencyProperty.Register(
            nameof(NotifyAboutNews), typeof(INotifyAboutNews), typeof(AddNewsNotificationBehavior), new PropertyMetadata(default(INotifyAboutNews), OnNotifyAboutNewsChanged));

        private static void OnNotifyAboutNewsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (AddNewsNotificationBehavior) d;
            
            if (e.OldValue != null)
                ((INotifyAboutNews) e.OldValue).NewsAdded -= sender.OnNotify;

            if (e.NewValue != null)
                ((INotifyAboutNews)e.NewValue).NewsAdded += sender.OnNotify;
        }

        public INotifyAboutNews NotifyAboutNews
        {
            get => (INotifyAboutNews) GetValue(NotifyAboutNewsProperty);
            set => SetValue(NotifyAboutNewsProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (NotifyAboutNews != null)
                NotifyAboutNews.NewsAdded += OnNotify;
        }

        private void OnNotify(object sender, IReadOnlyCollection<NewsModel> e)
        {
            Tools.DispatchedInvoke(() =>
            {
                AssociatedObject.Message = $"Added {e.Count} news";
                AssociatedObject.Open();
            });
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (NotifyAboutNews != null)
                NotifyAboutNews.NewsAdded -= OnNotify;
        }
    }
}
