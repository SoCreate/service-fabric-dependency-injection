using System.Fabric;
using Microsoft.ServiceFabric.Services.Runtime;

namespace SoCreate.ServiceFabric.DependencyInjection.Services
{
    public interface IStatefulServiceCreatorFactory
    {
        StatefulService Create(StatefulServiceContext context);
    }
}
