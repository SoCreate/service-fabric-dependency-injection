using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Fabric;

namespace SoCreate.ServiceFabric.DependencyInjection.Services
{
    public static class ServiceFabricRegistrationExtensions
    {
        public static IHostBuilder UseServiceFabricStatelessServiceFactory<TStatelessServiceFactory>(this IHostBuilder hostBuilder, string serviceTypeName, ReliableServiceFabricRegistrationOptions options = null)
        where TStatelessServiceFactory : class, IStatelessServiceCreatorFactory
        {
            options = options ?? new ReliableServiceFabricRegistrationOptions { ServiceTypeName = serviceTypeName };

            return hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(options);
                services.AddSingleton<IStatelessServiceCreatorFactory, TStatelessServiceFactory>();
                services.AddHostedService<ServiceFabricHostedService>();
            });
        }

        public static IHostBuilder UseServiceFabricStatelessServiceFactory(this IHostBuilder hostBuilder, string serviceTypeName, Func<StatelessServiceContext, StatelessService> serviceFactory, ReliableServiceFabricRegistrationOptions options = null)
        {
            options = options ?? new ReliableServiceFabricRegistrationOptions { ServiceTypeName = serviceTypeName };

            return hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(options);
                services.AddSingleton(serviceFactory);
                services.AddSingleton<IStatelessServiceCreatorFactory, StatelessServiceFactory>();
                services.AddHostedService<ServiceFabricHostedService>();
            });
        }

        public static IHostBuilder UseServiceFabricStatefulServiceFactory<TStatefulServiceFactory>(this IHostBuilder hostBuilder, string serviceTypeName, ReliableServiceFabricRegistrationOptions options = null)
        where TStatefulServiceFactory : class, IStatefulServiceCreatorFactory
        {
            options = options ?? new ReliableServiceFabricRegistrationOptions { ServiceTypeName = serviceTypeName };

            return hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(options);
                services.AddSingleton<IStatefulServiceCreatorFactory, TStatefulServiceFactory>();
                services.AddHostedService<ServiceFabricHostedService>();
            });
        }

        public static IHostBuilder UseServiceFabricStatefulServiceFactory(this IHostBuilder hostBuilder, string serviceTypeName, Func<StatefulServiceContext, StatefulService> serviceFactory, ReliableServiceFabricRegistrationOptions options = null)
        {
            options = options ?? new ReliableServiceFabricRegistrationOptions { ServiceTypeName = serviceTypeName };

            return hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(options);
                services.AddSingleton(serviceFactory);
                services.AddSingleton<IStatefulServiceCreatorFactory, StatefulServiceFactory>();
                services.AddHostedService<ServiceFabricHostedService>();
            });
        }
    }
}
