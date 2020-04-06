using Microsoft.Extensions.DependencyInjection;

namespace News.CoreModule
{
    public static class Configurator
    {
        public static IServiceCollection ConfigureCore(this IServiceCollection services)
        {
            return services;
        }
    }
}
