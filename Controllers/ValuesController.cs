using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace shopingWebServicesMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet("v1")]
        public ActionResult<IEnumerable<string>> GetV1()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet("v2")]
        public ActionResult<IEnumerable<string>> GetV2()
        {
            return new string[] { "value3", "value4" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("V3/{id}")]
        public string Post(int id, [FromBody] string value)
        {
            return value + " " + id.ToString();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
