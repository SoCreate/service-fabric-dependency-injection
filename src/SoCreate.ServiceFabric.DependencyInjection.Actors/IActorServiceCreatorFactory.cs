using System.Fabric;
using Microsoft.ServiceFabric.Actors.Runtime;

namespace SoCreate.ServiceFabric.DependencyInjection.Actors
{
    public interface IActorServiceCreatorFactory
    {
        ActorService Create(StatefulServiceContext context, ActorTypeInformation actorType);
    }
}
