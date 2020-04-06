﻿using Microsoft.Extensions.DependencyInjection;
using News.DataAccess.Interfaces;
using News.DataAccess.Repositories;

namespace News.DataAccess
{
    public static class Configurator
    {
        public static IServiceCollection ConfigureDataAccess(this IServiceCollection services)
        {
            services.AddTransient<INewsRepository, NewsRepository>();
            return services;
        }
    }
}
