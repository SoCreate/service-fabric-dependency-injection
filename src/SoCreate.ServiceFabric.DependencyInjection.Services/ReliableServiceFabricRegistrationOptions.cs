using System;
using System.Threading;

namespace SoCreate.ServiceFabric.DependencyInjection.Services
{
    public class ReliableServiceFabricRegistrationOptions
    {
        public string ServiceTypeName { get; set; }
        public TimeSpan Timeout { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
