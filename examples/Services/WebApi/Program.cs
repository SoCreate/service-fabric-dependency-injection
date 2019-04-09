using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using SoCreate.ServiceFabric.DependencyInjection.Services;

namespace WebApi
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
                .UseServiceFabricStatelessServiceFactory("WebApiType", context => new WebApi(context))
                .Build();
        }
    }
}
