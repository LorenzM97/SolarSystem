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
        public ObservableCollection<MovingSpaceObject> _listPlanets = new ObservableCollection<MovingSpaceObject>();

        public Solarsystem(string name)
        {
            Name = name;
        }

    }
}
