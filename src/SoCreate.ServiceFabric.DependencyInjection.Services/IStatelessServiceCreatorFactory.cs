using System.Fabric;
using Microsoft.ServiceFabric.Services.Runtime;

namespace SoCreate.ServiceFabric.DependencyInjection.Services
{
    public interface IStatelessServiceCreatorFactory
    {
        StatelessService Create(StatelessServiceContext context);
    }
}
