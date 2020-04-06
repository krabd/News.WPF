using System.Windows.Input;
using News.CoreModule.Interfaces;
using News.CoreModule.ViewModels;
using Prism.Commands;

namespace News.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IWindowService WindowService { get; set; }

        public ICommand StartCommand { get; }

        public MainWindowViewModel()
        {
            StartCommand = new DelegateCommand(OnStart);
        }

        private void OnStart()
        {
            
        }
    }
}