using System;
using System.Windows.Input;
using News.CoreModule.Interfaces;
using News.CoreModule.ViewModels;
using News.Utils.Extensions;
using Prism.Commands;

namespace News.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IServiceProvider _provider;

        public IWindowService WindowService { get; set; }

        public ICommand StartCommand { get; }

        public MainWindowViewModel(IServiceProvider provider)
        {
            _provider = provider;

            StartCommand = new DelegateCommand(OnStart);
        }

        private void OnStart()
        {
            var workspace = _provider.Resolve<WorkspaceViewModel>();
            workspace.InitializeAsync();
            WindowService.Show(workspace, "Hot news 2020");
        }
    }
}