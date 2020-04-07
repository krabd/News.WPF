using System.ComponentModel;
using News.CoreModule.Enums;

namespace News.CoreModule.Interfaces
{
    public interface IWindowService
    {
        void Show(INotifyPropertyChanged dataContext, string title);

        bool? ShowDialog(INotifyPropertyChanged dataContext, string title);

        bool? ShowMessage(string message, string title, NotificationResult resultType = NotificationResult.YesNo);
    }
}
