using System;
using Microsoft.Extensions.DependencyInjection;
using News.CoreModule;
using News.DataAccess;
using News.Interfaces;
using News.Services;
using News.ViewModels;

namespace News
{
    public class StartupService
    {
        public void Configure(IServiceCollection services)
        {
            ConfigureViewModels(services);
            ConfigureServices(services);
            ConfigureModules(services);
        }

        private void ConfigureViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();

            services.AddTransient<WorkspaceViewModel>();
            services.AddTransient<NewsViewModel>();
            services.AddTransient<CurrencyExchangeViewModel>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUpdateNewsService, UpdateNewsService>();
        }

        private void ConfigureModules(IServiceCollection services)
        {
            services.ConfigureCore();
            services.ConfigureDataAccess();
        }

        public IServiceProvider BuildProvider(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }
    }
}
