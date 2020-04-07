using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interactivity;
using News.Domain.Models;
using News.Interfaces;

namespace News.Behaviors
{
    public class AddNewsNotificationBehavior : Behavior<UIElement>
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

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (NotifyAboutNews != null)
                NotifyAboutNews.NewsAdded -= OnNotify;
        }
    }
}
