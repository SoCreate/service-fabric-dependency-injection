using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ServiceFabric.Actors.Runtime;
using System;
using System.Fabric;

namespace SoCreate.ServiceFabric.DependencyInjection.Actors
{
    public static class ServiceFabricRegistrationExtensions
    {
        public static IHostBuilder UseServiceFabricActorServiceFactory<TActor, TActorServiceFactory>(this IHostBuilder hostBuilder, ActorServiceFabricRegistrationOptions options = null)
            where TActor : ActorBase
            where TActorServiceFactory : class, IActorServiceCreatorFactory
        {
            options = options ?? new ActorServiceFabricRegistrationOptions();

            return hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(options);
                services.AddSingleton<IActorServiceCreatorFactory, TActorServiceFactory>();
                services.AddHostedService<ActorServiceFabricHostedService<TActor>>();
            });
        }

        public static IHostBuilder UseServiceFabricActorServiceFactory<TActor>(this IHostBuilder hostBuilder, Func<StatefulServiceContext, ActorTypeInformation, ActorService> serviceFactory, ActorServiceFabricRegistrationOptions options = null)
            where TActor : ActorBase
        {
            options = options ?? new ActorServiceFabricRegistrationOptions();

            return hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(options);
                services.AddSingleton(serviceFactory);
                services.AddSingleton<IActorServiceCreatorFactory, ActorServiceFactory>();
                services.AddHostedService<ActorServiceFabricHostedService<TActor>>();
            });
        }

        public static IHostBuilder UseServiceFabricActorServiceFactory<TActor>(this IHostBuilder hostBuilder, ActorServiceFabricRegistrationOptions options = null)
            where TActor : ActorBase
        {
            options = options ?? new ActorServiceFabricRegistrationOptions();

            return hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(options);
                services.AddSingleton<Func<StatefulServiceContext, ActorTypeInformation, ActorService>>((context, actorType) => new ActorService(context, actorType));
                services.AddSingleton<IActorServiceCreatorFactory, ActorServiceFactory>();
                services.AddHostedService<ActorServiceFabricHostedService<TActor>>();
            });
        }
    }
}
