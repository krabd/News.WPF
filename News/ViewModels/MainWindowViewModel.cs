using System.Windows.Input;
using News.CoreModule.Interfaces;
using News.CoreModule.ViewModels;
using Prism.Commands;

namespace News.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IWorkspaceService _workspaceService;

        public ICommand StartCommand { get; }

        public MainWindowViewModel(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;

            StartCommand = new DelegateCommand(OnStart);
        }

        private void OnStart()
        {
            _workspaceService.OpenIndependenceWorkspace<WorkspaceViewModel>("Hot news 2020");
        }
    }
}