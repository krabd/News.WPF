using Microsoft.Extensions.DependencyInjection;
using News.CoreModule.Interfaces;
using News.CoreModule.Services;

namespace News.CoreModule
{
    public static class Configurator
    {
        public static IServiceCollection ConfigureCore(this IServiceCollection services)
        {
            services.AddSingleton<IWorkspaceService, WorkspaceService>();
            return services;
        }
    }
}
