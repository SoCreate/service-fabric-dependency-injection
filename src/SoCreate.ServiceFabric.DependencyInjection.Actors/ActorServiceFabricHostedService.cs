﻿using System;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace SoCreate.ServiceFabric.DependencyInjection.Actors
{
    internal class ActorServiceFabricHostedService<TActor> : IHostedService where TActor : ActorBase
    {
        private readonly IActorServiceCreatorFactory _actorServiceFactory;
        private readonly Action<ServiceContext> _addServiceContextToLogging;
        private readonly ActorServiceFabricRegistrationOptions _registrationOptions;
        private readonly ILogger<ActorServiceFabricHostedService<TActor>> _logger;

        public ActorServiceFabricHostedService(
            IServiceProvider serviceProvider,
            IActorServiceCreatorFactory actorServiceFactory,
            ActorServiceFabricRegistrationOptions registrationOptions,
            ILogger<ActorServiceFabricHostedService<TActor>> logger)
        {
            _actorServiceFactory = actorServiceFactory;
            _registrationOptions = registrationOptions;
            _logger = logger;
            _addServiceContextToLogging = serviceProvider.GetService<Action<ServiceContext>>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                Func<StatefulServiceContext, ActorTypeInformation, ActorService> create = (context, actorType) => {
                    _addServiceContextToLogging?.Invoke(context);
                    return _actorServiceFactory.Create(context, actorType);
                };

                await ActorRuntime.RegisterActorAsync<TActor>(
                    create,
                    _registrationOptions.Timeout = default,
                    _registrationOptions.CancellationToken = default
                    );

                _logger.LogInformation($"Actor service type {typeof(TActor).Name} is registered.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Actor service host initialization failed for {typeof(TActor).Name}.");
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Actor service host is stopping for {typeof(TActor).Name}.");
            return Task.CompletedTask;
        }
    }
}
