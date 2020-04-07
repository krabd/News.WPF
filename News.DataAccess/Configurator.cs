using System.Configuration;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using News.DataAccess.Interfaces;
using News.DataAccess.Repositories;
using News.Utils.Extensions;

namespace News.DataAccess
{
    public static class Configurator
    {
        public static IServiceCollection ConfigureDataAccess(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddTransient<INewsRepository, NewsRepository>(provider => new NewsRepository(
                provider.Resolve<IHttpClientFactory>(),
                ConfigurationManager.AppSettings.Get("newsBaseUrl"),
                ConfigurationManager.AppSettings.Get("newsApiKey")));

            services.AddTransient<ICurrencyExchangeRepository, CurrencyExchangeRepository>(provider => new CurrencyExchangeRepository(
                provider.Resolve<IHttpClientFactory>(),
                ConfigurationManager.AppSettings.Get("currencyExchangeBaseUrl")));

            return services;
        }
    }
}
