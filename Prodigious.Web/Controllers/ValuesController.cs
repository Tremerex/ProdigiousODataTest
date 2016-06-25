using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Prodigious.Web.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public async Task<IHttpActionResult> Get()
        {
            return await Task.Run(() => Ok(getDelay()));
        }

        private int getDelay()
        {
            int x = 0;
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 40000000; j++)
                {
                    x = j;
                }
            }
            return x;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
