using System.Windows.Input;
using News.CoreModule.Interfaces;
using News.CoreModule.ViewModels;
using Prism.Commands;

namespace News.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IWindowService WindowService { get; set; }

        public NewsViewModel News { get; }

        public ICommand StartCommand { get; }

        public MainWindowViewModel(NewsViewModel news)
        {
            News = news;
            StartCommand = new DelegateCommand(OnStart);
        }

        private void OnStart()
        {
            WindowService.Show(News, "Hot news 2020");
        }
    }
}