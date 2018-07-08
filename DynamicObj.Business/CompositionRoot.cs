using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DynamicObj.Share.DTO;
using DynamicObj.Share.Module;
using LightInject;

[assembly: CompositionRootType(typeof(DynamicObj.Business.CompositionRoot))]
namespace DynamicObj.Business
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
                cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                cfg.CreateMap<Foo, FooDTO>().ForMember(desc => desc.Address_title, source => source.MapFrom(s => s.Address)).ReverseMap();
            }
            );



            serviceRegistry.Register<FooService>();
        }
    }

}
