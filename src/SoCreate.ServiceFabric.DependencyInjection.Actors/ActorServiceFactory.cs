using System;
using System.Fabric;
using Microsoft.ServiceFabric.Actors.Runtime;

namespace SoCreate.ServiceFabric.DependencyInjection.Actors
{
    internal class ActorServiceFactory : IActorServiceCreatorFactory
    {
        private readonly Func<StatefulServiceContext, ActorTypeInformation, ActorService> _serviceFactory;

        public ActorServiceFactory(Func<StatefulServiceContext, ActorTypeInformation, ActorService> serviceFactory, IServiceProvider serviceProvider)
        {
            _serviceFactory = serviceFactory;
        }

        public ActorService Create(StatefulServiceContext context, ActorTypeInformation actorType)
        {
            return _serviceFactory(context, actorType);
        }
    }
}
