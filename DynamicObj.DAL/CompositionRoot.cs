using LightInject;
using System.Collections.Generic;

[assembly: CompositionRootType(typeof(DynamicObj.DAL.CompositionRoot))]
namespace DynamicObj.DAL
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ConnectionFactory>();
            serviceRegistry.Register<BaseObjRepository>();
        }
    }

    public class EquaCo : IEqualityComparer<ServiceRegistration>
    {
        public bool Equals(ServiceRegistration x, ServiceRegistration y)
        {
            if (x != null && y != null)
            {
                return x.ServiceName == y.ServiceName;
            }
            return false;
        }

        public int GetHashCode(ServiceRegistration obj)
        {
            return obj.GetHashCode();
        }
    }

}
