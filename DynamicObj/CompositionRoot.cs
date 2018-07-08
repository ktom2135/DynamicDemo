using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicObj.Share;
using LightInject;

[assembly: CompositionRootType(typeof(DynamicObj.DAL.CompositionRoot))]
namespace DynamicObj.DAL
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
        }
    }
}
