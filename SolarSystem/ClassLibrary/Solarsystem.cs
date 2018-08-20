using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Library_Solarsystem
{
    public class Solarsystem : Galaxy, INotifyPropertyChanged
    {
        protected string name;
        private ObservableCollection<SpaceObject> listPlanets = new ObservableCollection<SpaceObject>();

        public string Name { get => name; set => name = value; }
        public ObservableCollection<SpaceObject> ListPlanets { get => listPlanets; set => listPlanets = value; }

        public Solarsystem()
        {

        }

        public Solarsystem(string name)
        {
            Name = name;
        }

       
    }
}
