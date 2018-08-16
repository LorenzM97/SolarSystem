using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Library_Solarsystem
{
    public class Solarsystem
    {
        public string Name;
        public ObservableCollection<SpaceObject> _listPlanets = new ObservableCollection<SpaceObject>();

        public Solarsystem(string name)
        {
            Name = name;
        }

    }
}
