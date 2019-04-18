using System;
using System.Fabric;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace SoCreate.ServiceFabric.DependencyInjection.Services
{
    internal class StatelessServiceFactory : IStatelessServiceCreatorFactory
    {
        private readonly Func<StatelessServiceContext, StatelessService> _serviceFactory;
        private readonly Action<ServiceContext> _addServiceContextToLogging;

        public StatelessServiceFactory(Func<StatelessServiceContext, StatelessService> serviceFactory, IServiceProvider serviceProvider)
        {
            _serviceFactory = serviceFactory;
            _addServiceContextToLogging = serviceProvider.GetService<Action<ServiceContext>>();
        }

        public StatelessService Create(StatelessServiceContext context)
        {
            _addServiceContextToLogging?.Invoke(context);

            return _serviceFactory(context);
        }
    }
}
