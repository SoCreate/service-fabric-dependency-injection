using System;
using System.Fabric;
using Microsoft.ServiceFabric.Services.Runtime;

namespace SoCreate.ServiceFabric.DependencyInjection.Services
{
    internal class StatelessServiceFactory : IStatelessServiceCreatorFactory
    {
        private readonly Func<StatelessServiceContext, StatelessService> _serviceFactory;

        public StatelessServiceFactory(Func<StatelessServiceContext, StatelessService> serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public StatelessService Create(StatelessServiceContext context)
        {
            return _serviceFactory(context);
        }
    }
}
