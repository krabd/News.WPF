using News.CoreModule.Enums;

namespace News.CoreModule.Interfaces
{
    public interface IWorkspaceService
    {
        void OpenWorkspace<T>(string title) where T : IWorkspace;

        void OpenIndependenceWorkspace<T>(string title) where T : IWorkspace;
    }
}
