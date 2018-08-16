using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary;
using Microsoft.AspNetCore.Mvc;

namespace SolarSystem.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        List<MovingSpaceObject> _sunsystems = new List<MovingSpaceObject>() {
            new MovingSpaceObject() { Name = "test", _childMoons = { new MovingSpaceObject() { Name = "m1" }, new MovingSpaceObject() { Name = "m2" } } },
            new MovingSpaceObject() { Name = "test2", _childMoons = { new MovingSpaceObject() { Name = "m3" } } } };

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return from a in _sunsystems select a.Name;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public MovingSpaceObject Get(string id)
        {
            return (from a in _sunsystems where a.Name == id select a).First();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
