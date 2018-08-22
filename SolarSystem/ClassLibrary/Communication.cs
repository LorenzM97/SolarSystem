using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Library_Solarsystem
{
    public class Communication
    {
        public ObservableCollection<SpaceObject> getList()
        {
            string jsonText = File.ReadAllText("../../../jsonSolarsystems.txt");
            return JsonConvert.DeserializeObject<ObservableCollection<SpaceObject>>(jsonText);
        }
    }
}
