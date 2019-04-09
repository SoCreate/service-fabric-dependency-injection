using System;
using System.Threading;

namespace SoCreate.ServiceFabric.DependencyInjection.Actors
{
    public class ActorServiceFabricRegistrationOptions
    {
        public TimeSpan Timeout { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
