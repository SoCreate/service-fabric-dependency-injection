# Service Fabric Dependency Injection
Enables you to easily use the .Net Core Host Builder in your Service Fabric template to get dependency injection working within you services.  This also makes it easy to use .Net Core configuration and logging working with your services.

![Build Status](https://dev.azure.com/SoCreate/Service%20Fabric%20Dependency%20Injection/_apis/build/status/SoCreate.service-fabric-dependency-injection?branchName=master)
[![NuGet Badge](https://buildstats.info/nuget/SoCreate.ServiceFabric.DependencyInjection.Services)](https://www.nuget.org/packages/SoCreate.ServiceFabric.DependencyInjection.Services/)
[![NuGet Badge](https://buildstats.info/nuget/SoCreate.ServiceFabric.DependencyInjection.Actors)](https://www.nuget.org/packages/SoCreate.ServiceFabric.DependencyInjection.Actors/)

## Installation

Reliable Services:

    dotnet add package SoCreate.ServiceFabric.DependencyInjection.Services

Reliable Actors:

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

### Actor 
```
    .UseServiceFabricActorServiceFactory<ActorTest>()
```

More complete example of the "Program.cs" file:
```
    internal static class Program
    {
        private static async Task Main()
        {
            await CreateHost().RunAsync();
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


### License

Service Fabric Dependency Injection is [MIT licensed](./LICENSE).