using System.ComponentModel;

namespace News.CoreModule.Interfaces
{
    public interface IWindowService
    {
        void Show(INotifyPropertyChanged dataContext, string title);

        bool? ShowDialog(INotifyPropertyChanged dataContext, string title);
    }
}
