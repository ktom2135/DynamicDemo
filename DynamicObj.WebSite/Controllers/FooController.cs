using DynamicObj.Business;
using DynamicObj.Share.DTO;
using LightInject;
using System.Collections.Generic;
using System.Web.Http;

namespace DynamicObj.WebSite.Controllers
{
    public class FooController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public FooDTO Get(int id)
        {

            var fooService = DIContainer.container.GetInstance<FooService>();
            return fooService.GetById(id);
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}