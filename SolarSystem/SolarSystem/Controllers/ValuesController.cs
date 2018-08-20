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
        //Galaxy galaxy = new Galaxy();
        //public ObservableCollection<Solarsystem> _sunsystems = new ObservableCollection<Solarsystem>();
        public ObservableCollection<Solarsystem> _sunsystems = new ObservableCollection<Solarsystem>() {
            new Solarsystem("Erdsystem") { Name = "Erdsystem",
                ListPlanets = {
                    new SpaceObject("planet", "Venus", 20, 20, 0) {
                        ListMoons = {
                            new SpaceObject("Mond1") { Name = "m1" },
                            new SpaceObject("Mond2") { Name = "m2" }
                        }
                    },
                    new SpaceObject("planet", "earth", 10, 30, 0) {
                        ListMoons = {
                            new SpaceObject() { Name = "m3" }
                        }
                    }
                }
            },
            new Solarsystem("Alpha Centauri") {
                ListPlanets = {
                    new SpaceObject("planet", "XAMK", 20, 20, 0) {
                        ListMoons = {
                            new SpaceObject() { Name = "m1" },
                            new SpaceObject() { Name = "m2" }
                        }
                    },
                    new SpaceObject("planet", "asf", 10, 30, 0) {
                        ListMoons = {
                            new SpaceObject() { Name = "m3" }
                        }
                    }
                }
            },
            new Solarsystem("Proxima Centauri")
        };

        // GET api/values
        [HttpGet]
        public IEnumerable<Solarsystem> Get()
        {
            //galaxy.ListSystems = _sunsystems;
            //return from a in _sunsystems select a.Name;
            
            return _sunsystems;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Solarsystem Get(string id)
        {
            return (from a in _sunsystems where a.Name == id select a).First();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ObservableCollection<Solarsystem> value)
        {
            _sunsystems = value;
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
