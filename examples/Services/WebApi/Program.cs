using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using SoCreate.ServiceFabric.DependencyInjection.Services;
using Microsoft.Extensions.Logging;

namespace WebApi
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
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                })
                .ConfigureServices((context, services) =>
                {
                })
                .ConfigureLogging(b => b.AddDebug())
                .UseServiceFabricStatelessServiceFactory<StatelessServiceProvider>("WebApiType")
                .Build();
        }
    }
}
