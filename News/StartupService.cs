using System;
using Microsoft.Extensions.DependencyInjection;
using News.ViewModels;

namespace News
{
    public class StartupService
    {
        public void Configure(IServiceCollection services)
        {
            ConfigureServices(services);
            ConfigureViewModels(services);
            ConfigureModules(services);
        }

        private void ConfigureServices(IServiceCollection services)
        {
        }

        private void ConfigureViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<NewsViewModel>();
        }

        private void ConfigureModules(IServiceCollection services)
        {
            new CoreModule.Configurator().Configure(services);
        }

        public IServiceProvider BuildProvider(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }
    }
}
