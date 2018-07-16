using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Enigmatry.Blueprint.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post(string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}