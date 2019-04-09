using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace SoCreate.ServiceFabric.DependencyInjection.Services
{
    internal class ServiceFabricHostedService : IHostedService
    {
        private readonly IStatelessServiceCreatorFactory _statelessServiceFactory;
        private readonly IStatefulServiceCreatorFactory _statefulServiceFactory;
        private readonly ReliableServiceFabricRegistrationOptions _registrationOptions;
        private readonly ILogger<ServiceFabricHostedService> _logger;

        public ServiceFabricHostedService(
            IServiceProvider serviceProvider,
            ReliableServiceFabricRegistrationOptions registrationOptions,
            ILogger<ServiceFabricHostedService> logger)
        {
            _statelessServiceFactory = serviceProvider.GetService<IStatelessServiceCreatorFactory>();
            _statefulServiceFactory = serviceProvider.GetService<IStatefulServiceCreatorFactory>();
            _registrationOptions = registrationOptions;
            _logger = logger;

            if (string.IsNullOrEmpty(registrationOptions.ServiceTypeName))
            {
                throw new Exception("Service Fabric service must have a ServiceTypeName set on the ServiceFabricRegistrationOptions.");
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (_statelessServiceFactory != null)
                {
                    await ServiceRuntime.RegisterServiceAsync(
                        _registrationOptions.ServiceTypeName,
                        _statelessServiceFactory.Create,
                        _registrationOptions.Timeout = default,
                        _registrationOptions.CancellationToken = default
                        );
                }
                else
                {
                    await ServiceRuntime.RegisterServiceAsync(
                        _registrationOptions.ServiceTypeName,
                        _statefulServiceFactory.Create,
                        _registrationOptions.Timeout = default,
                        _registrationOptions.CancellationToken = default
                        );
                }

                _logger.LogInformation($"Service type {_registrationOptions.ServiceTypeName} is registered.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Service host initialization failed for {_registrationOptions.ServiceTypeName}.");
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Service host is stopping for {_registrationOptions.ServiceTypeName}.");
            return Task.CompletedTask;
        }
    }
}
