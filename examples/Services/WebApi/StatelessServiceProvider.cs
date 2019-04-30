using SoCreate.ServiceFabric.DependencyInjection.Services;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Fabric;
using Microsoft.Extensions.Logging;

namespace WebApi
{
    class StatelessServiceProvider : IStatelessServiceCreatorFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILoggerProvider _loggerProvider;

        public StatelessServiceProvider(ILoggerFactory loggerFactory, ILoggerProvider loggerProvider)
        {
            _loggerFactory = loggerFactory;
            _loggerProvider = loggerProvider;
        }

        public StatelessService Create(StatelessServiceContext context)
        {
            return new WebApi(context, _loggerFactory.CreateLogger<WebApi>(), _loggerProvider);
        }
    }
}
