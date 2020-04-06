using System.Windows.Input;
using News.CoreModule.Interfaces;
using News.CoreModule.ViewModels;
using Prism.Commands;

namespace News.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IWindowService WindowService { get; set; }

        public GeneralViewModel General { get; }

        public ICommand StartCommand { get; }

        public MainWindowViewModel(GeneralViewModel general)
        {
            General = general;

            StartCommand = new DelegateCommand(OnStart);
        }

        private void OnStart()
        {
            WindowService.Show(General, "Hot news 2020");
        }
    }
}