using System;
using Microsoft.Extensions.DependencyInjection;
using News.ViewModels;

namespace News
{
    public class StartupService
    {
        public void Configure(IServiceCollection services)
        {
            ConfigureViewModels(services);
            ConfigureModules(services);
        }

        private void ConfigureViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();

            services.AddTransient<WorkspaceViewModel>();
            services.AddTransient<NewsViewModel>();
            services.AddTransient<CurrencyExchangeViewModel>();
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
