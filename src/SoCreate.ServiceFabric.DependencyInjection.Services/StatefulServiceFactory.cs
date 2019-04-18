using System;
using System.Fabric;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace SoCreate.ServiceFabric.DependencyInjection.Services
{
    internal class StatefulServiceFactory : IStatefulServiceCreatorFactory
    {
        private readonly Func<StatefulServiceContext, StatefulService> _serviceFactory;
        private readonly Action<ServiceContext> _addServiceContextToLogging;
        
        public StatefulServiceFactory(Func<StatefulServiceContext, StatefulService> serviceFactory, IServiceProvider serviceProvider)
        {
            _serviceFactory = serviceFactory;
            _addServiceContextToLogging = serviceProvider.GetService<Action<ServiceContext>>();
        }

        public StatefulService Create(StatefulServiceContext context)
        {
            _addServiceContextToLogging?.Invoke(context);

            return _serviceFactory(context);
        }
    }
}
