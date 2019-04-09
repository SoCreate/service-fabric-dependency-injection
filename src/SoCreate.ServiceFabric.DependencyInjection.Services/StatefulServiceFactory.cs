using System;
using System.Fabric;
using Microsoft.ServiceFabric.Services.Runtime;

namespace SoCreate.ServiceFabric.DependencyInjection.Services
{
    internal class StatefulServiceFactory : IStatefulServiceCreatorFactory
    {
        private readonly Func<StatefulServiceContext, StatefulService> _serviceFactory;

        public StatefulServiceFactory(Func<StatefulServiceContext, StatefulService> serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public StatefulService Create(StatefulServiceContext context)
        {
            return _serviceFactory(context);
        }
    }
}
