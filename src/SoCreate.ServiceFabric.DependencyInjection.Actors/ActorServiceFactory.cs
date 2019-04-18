using System;
using System.Fabric;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace SoCreate.ServiceFabric.DependencyInjection.Actors
{
    internal class ActorServiceFactory : IActorServiceCreatorFactory
    {
        private readonly Func<StatefulServiceContext, ActorTypeInformation, ActorService> _serviceFactory;
        private readonly Action<ServiceContext> _addServiceContextToLogging;

        public ActorServiceFactory(Func<StatefulServiceContext, ActorTypeInformation, ActorService> serviceFactory, IServiceProvider serviceProvider)
        {
            _serviceFactory = serviceFactory;
            _addServiceContextToLogging = serviceProvider.GetService<Action<ServiceContext>>();
        }

        public ActorService Create(StatefulServiceContext context, ActorTypeInformation actorType)
        {
            _addServiceContextToLogging?.Invoke(context);

            return _serviceFactory(context, actorType);
        }
    }
}
