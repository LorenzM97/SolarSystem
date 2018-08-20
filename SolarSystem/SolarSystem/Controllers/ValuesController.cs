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
                   new SpaceObject("sun", "Sonne", 100)
                    {

                    },
                    new SpaceObject("planet", "Venus", 80, 80, 0.0) {
                        ListMoons = {
                            new SpaceObject("moon", "Mond1", 10, 20, Math.PI) {  },
                            new SpaceObject("moon", "Mond2", 20, 30, 0.0) { }
                        }
                    },
                    new SpaceObject("planet", "earth",  200, 400, 0.0) {
                        ListMoons = {
                            new SpaceObject("moon", "Mond3", 50, 30, Math.PI) {  }
                        }
                    }
                } } };
        //    },
        //    new Solarsystem("Alpha Centauri") {
        //        ListPlanets = {
        //            new SpaceObject("planet", "XAMK", 20, 20, 0) {
        //                ListMoons = {
        //                    new SpaceObject("moon", "Mond4", 24, 12, 1 / 2 * Math.PI) {  },
        //                    new SpaceObject("moon", "Mond5", 24, 12, Math.PI) {  }
        //                }
        //            },
        //            new SpaceObject("planet", "asf", 10, 30, 0) {
        //                ListMoons = {
        //                    new SpaceObject("moon", "Mond6", 24, 12, Math.PI) {  }
        //                }
        //            }
        //        }
        //    },
        //    new Solarsystem("Proxima Centauri")
        //};

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
