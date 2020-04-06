using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace News.Utils.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static T Resolve<T>(this IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var service = serviceProvider.GetService<T>();
            if (service == null)
            {
                throw new InvalidOperationException($"Unable to resolve type: '{typeof(T)}'");
            }

            return service;
        }

        public static object Resolve(this IServiceProvider serviceProvider, Type type)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var service = serviceProvider.GetService(type);
            if (service == null)
            {
                throw new InvalidOperationException($"Unable to resolve type: '{type.FullName}'");
            }

            return service;
        }

        /// <summary>
        /// Warning: Все сущности типа <typeparamref name="TInterface" /> будут созданы при вызове данного метода
        /// </summary>
        public static TInterface Resolve<TInterface, TImplementation>(this IServiceProvider serviceProvider)
        {
            var services = serviceProvider.ResolveAll<TInterface>();

            var service = services.FirstOrDefault(s => s is TImplementation);
            if (service == null)
            {
                throw new InvalidOperationException($"Unable to resolve implementation: '{typeof(TImplementation)}' of the type: '{typeof(TInterface)}'");
            }

            return service;
        }

        public static TInterface Resolve<TInterface>(this IServiceProvider serviceProvider, Type implementationType)
        {
            var services = serviceProvider.ResolveAll<TInterface>();

            var service = services.FirstOrDefault(s => s.GetType() == implementationType);
            if (service == null)
            {
                throw new InvalidOperationException($"Unable to resolve implementation: '{implementationType}' of the type: '{typeof(TInterface)}'");
            }

            return service;
        }

        public static IEnumerable<T> ResolveAll<T>(this IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var services = serviceProvider.GetServices<T>().ToArray();
            if (services == null || !services.Any())
            {
                throw new InvalidOperationException($"Unable to resolve type: '{typeof(T)}'");
            }

            return services;
        }

        public static T Resolve<T>(this IServiceProvider serviceProvider, params object[] parameters)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var service = ActivatorUtilities.CreateInstance(serviceProvider, typeof(T), parameters);
            if (service == null)
            {
                throw new InvalidOperationException($"Unable to resolve type: '{typeof(T)}'");
            }

            return (T)service;
        }

        public static T Resolve<T, TParameter>(this IServiceProvider serviceProvider, TParameter parameter)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var service = ActivatorUtilities.CreateInstance<T>(serviceProvider, parameter);
            if (service == null)
            {
                throw new InvalidOperationException($"Unable to resolve type: '{typeof(T)}'");
            }

            return service;
        }

        public static T Resolve<T, TParameter1, TParameter2>(this IServiceProvider serviceProvider, TParameter1 parameter1, TParameter2 parameter2)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var service = ActivatorUtilities.CreateInstance<T>(serviceProvider, parameter1, parameter2);
            if (service == null)
            {
                throw new InvalidOperationException($"Unable to resolve type: '{typeof(T)}'");
            }

            return service;
        }

        public static T Resolve<T, TParameter1, TParameter2, TParameter3>(this IServiceProvider serviceProvider, TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var service = ActivatorUtilities.CreateInstance<T>(serviceProvider, parameter1, parameter2, parameter3);
            if (service == null)
            {
                throw new InvalidOperationException($"Unable to resolve type: '{typeof(T)}'");
            }

            return service;
        }
    }
}
