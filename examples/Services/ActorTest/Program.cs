using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SoCreate.ServiceFabric.DependencyInjection.Actors;

namespace ActorTest
{
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
                })
                .ConfigureServices((context, services) =>
                {
                })
                .UseServiceFabricActorServiceFactory<ActorTest>()
                .Build();
        }
    }
}
