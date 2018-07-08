using AutoMapper;
using DynamicObj.DAL;
using DynamicObj.Share.DTO;
using DynamicObj.Share.Module;
using LightInject;
using System.Collections.Generic;
using System.Linq;

namespace DynamicObj.Business
{
    public class FooService
    {
        private BaseObjRepository _fooRepository;
        public FooService([Inject] BaseObjRepository fooRepository)
        {
            _fooRepository = fooRepository;
        }

        public FooDTO Insert(FooDTO foo)
        {
            Foo fooModule = Mapper.Map<Foo>(foo);
            var fooReturn = new Foo(_fooRepository.InsertBaseObj(fooModule));
            return Mapper.Map<FooDTO>(fooReturn);
        }

        public FooDTO GetById(int id)
        {
            var baseObj = _fooRepository.GetObj(id);

            Foo result = new Foo(baseObj);
            return Mapper.Map<FooDTO>(result);
        }

        public List<FooDTO> GetByIds(List<int> ids)
        {
            var baseObjs = _fooRepository.GetObjs(ids);

            return baseObjs.Select(a => Mapper.Map<FooDTO>(new Foo(a))).ToList();
        }
    }
}
