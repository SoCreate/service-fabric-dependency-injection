# Service Fabric Dependency Injection
Enables you to easily use the .Net Core Host Builder in your Service Fabric template to get dependency injection working within you services.  This also makes it easy to use .Net Core configuration and logging working with your services.

![Build Status](https://dev.azure.com/SoCreate/Service%20Fabric%20Dependency%20Injection/_apis/build/status/SoCreate.service-fabric-dependency-injection?branchName=master)

## Installation

### Reliable Services: 

[![NuGet Badge](https://buildstats.info/nuget/SoCreate.ServiceFabric.DependencyInjection.Services)](https://www.nuget.org/packages/SoCreate.ServiceFabric.DependencyInjection.Services/)


    dotnet add package SoCreate.ServiceFabric.DependencyInjection.Services


### Reliable Actors:

[![NuGet Badge](https://buildstats.info/nuget/SoCreate.ServiceFabric.DependencyInjection.Actors)](https://www.nuget.org/packages/SoCreate.ServiceFabric.DependencyInjection.Actors/)

    dotnet add package SoCreate.ServiceFabric.DependencyInjection.Actors


## Documentation

Add to HostBuilder:

#### Stateless Service

```
    .UseServiceFabricStatelessServiceFactory("StatelessType", context => new Stateless(context))
```

#### Stateful Service

```
    .UseServiceFabricStatefulServiceFactory("StatefulType", context => new Stateful(context))
```

#### Actor Service
```
    .UseServiceFabricActorServiceFactory<ActorTest>()
```

More complete example of the "Program.cs" file:
```
    internal static class Program
    {
        private static async Task Main()
        {
            using (var host = CreateHost())
            {
                await host.RunAsync();
            }
        }

        private static IHost CreateHost()
        {
            return new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile("appsettings.json", optional: true);
                    configApp.AddJsonFile(
                        $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true);
                })
                .ConfigureServices((context, services) =>
                {
                    service.AddSingleton<ISomeService, SomeService>();
                })
                .UseServiceFabricStatelessServiceFactory("StatelessType", context => new Stateless(context))
                .Build();
        }
    }
```

#### Example
This example show how to get a class registed with the dependency injection container into the constructor of your Stateless Service.  In this case the example class is "SomeService" which implements "ISomeService" but this could be any class registered with the dependency injection container.

MyStatelessService.cs
```
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;

namespace MyExample
{
    internal sealed class MyStatelessService : StatelessService
    {
        private readonly ISomeService _someService;

        public MyStatelessService(StatelessServiceContext context, ISomeService someService)
            : base(context)
        {
            _someService = someService;
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                _someService.DoSomething();

                await Task.Delay(TimeSpan.FromSeconds(20), cancellationToken);
            }
        }
    }
}
```

MyStatelessServiceFactory.cs
```
using System.Fabric;
using Microsoft.ServiceFabric.Services.Runtime;
using SoCreate.ServiceFabric.DependencyInjection.Services;

namespace MyExample
{
    class MyStatelessServiceFactory : IStatelessServiceCreatorFactory
    {
        private readonly ISomeService _someService;

        public MyStatelessServiceFactory(ISomeService someService)
        {
            _someService = someService;
        }

        public StatelessService Create(StatelessServiceContext context)
        {
            return new MyStatelessService(context, _someService);
        }
    }
}
```

Program.cs
```
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoCreate.ServiceFabric.DependencyInjection.Services;
using System.Threading.Tasks;

namespace MyExample
{
    internal static class Program
    {
        private static async Task Main()
        {
            using (var host = CreateHost())
            {
                await host.RunAsync();
            }
        }

        private static IHost CreateHost()
        {
            return new HostBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<ISomeService, SomeService>();
                })
                .UseServiceFabricStatelessServiceFactory<MyStatelessServiceFactory>("MyStatelessServiceType")
                .Build();
        }
    }
}

```

### License

Service Fabric Dependency Injection is [MIT licensed](./LICENSE).