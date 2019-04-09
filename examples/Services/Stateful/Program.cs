using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SoCreate.ServiceFabric.DependencyInjection.Services;

namespace Stateful
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
                .UseServiceFabricStatefulServiceFactory("StatefulType", context => new Stateful(context))
                .Build();
        }
    }
}
