using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary;
using Library_Solarsystem;
using Microsoft.AspNetCore.Mvc;

namespace SolarSystem.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        
        List<Solarsystem> _sunsystems = new List<Solarsystem>() {
            new Solarsystem("Erdsystem") { Name = "Erdsystem",
                _listPlanets = {
                    new SpaceObject("planet", "Venus", 20, 20, 0) {
                        _listMoons = {
                            new SpaceObject() { Name = "m1" },
                            new SpaceObject() { Name = "m2" }
                        }
                    },
                    new SpaceObject("planet", "earth", 10, 30, 0) {
                        _listMoons = {
                            new SpaceObject() { Name = "m3" }
                        }
                    }
                }
            },
            new Solarsystem("Alpha Centauri"),
            new Solarsystem("Proxima Centauri")
        };

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return from a in _sunsystems select a.Name;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Solarsystem Get(string id)
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
