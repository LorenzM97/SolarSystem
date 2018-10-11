using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.IO;
using Library_Solarsystem;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SolarSystem.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public ObservableCollection<Solarsystem> _sunsystems = new ObservableCollection<Solarsystem>();


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

        private async void SaveSystemList(int index)
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
                        System.IO.File.WriteAllText("../../../../../Index.txt", "" + index);
                    }
                    catch (Exception)
                    {
                        SaveSystemList(index);
                    }

                }
            }
        }
    }
}
