using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.IO;
using Library_Solarsystem;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ClassLibrary;

namespace SolarSystem.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public ObservableCollection<Solarsystem> _sunsystems = new ObservableCollection<Solarsystem>();


        //public ObservableCollection<Solarsystem> _sunsystems = new ObservableCollection<Solarsystem>() {
        //    new Solarsystem("Erdsystem") { Name = "Erdsystem",
        //        ListPlanets = {
        //            new SpaceObject("planet", "Venus", 20, 20, 0) {
        //                ListMoons = {
        //                    new SpaceObject() { Name = "m1" },
        //                    new SpaceObject() { Name = "m2" }
        //                }
        //            },
        //            new SpaceObject("planet", "earth", 10, 30, 0) {
        //                ListMoons = {
        //                    new SpaceObject() { Name = "m3" }
        //                }
        //            }
        //        }
        //    },
        //    new Solarsystem("Alpha Centauri") {
        //        ListPlanets = {
        //            new SpaceObject("planet", "XAMK", 20, 20, 0) {
        //                ListMoons = {
        //                    new SpaceObject() { Name = "m1" },
        //                    new SpaceObject() { Name = "m2" }
        //                }
        //            },
        //            new SpaceObject("planet", "asf", 10, 30, 0) {
        //                ListMoons = {
        //                    new SpaceObject() { Name = "m3" }
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
            LoadSystemList();
            return _sunsystems;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Solarsystem Get(string id)
        {
            LoadSystemList();
            return (from a in _sunsystems where a.Name == id select a).First();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ObservableCollection<Solarsystem> value)
        {
            _sunsystems = value;
            //SaveSystemList();
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

        private void LoadSystemList()
        {
            try
            {
                try
                {
                    string jsonText = System.IO.File.ReadAllText("../jsonSolarsystems.txt");
                    _sunsystems = JsonConvert.DeserializeObject<ObservableCollection<Solarsystem>>(jsonText);
                }
                catch (Exception)
                {
                    string jsonText = System.IO.File.ReadAllText("../jsonSolarsystemsSicherung.txt");
                    _sunsystems = JsonConvert.DeserializeObject<ObservableCollection<Solarsystem>>(jsonText);
                }
            }
            catch (Exception)
            {
                LoadSystemList();
            }
        }

        private async void SaveSystemList()
        {
            using (var c = new HttpClient() { })
            {
                HttpResponseMessage response = await c.GetAsync(new Uri($"http://localhost:59306/api/values"));
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("../", "jsonSolarsystems.txt")))
                        {
                            outputFile.Write(await response.Content.ReadAsStringAsync());
                        }
                    }
                    catch (Exception)
                    {
                        SaveSystemList();
                    }

                }
            }
        }
    }
}