using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Library_Solarsystem
{
    public class Solarsystem : INotifyPropertyChanged
    {
        private string name;
        public ObservableCollection<SpaceObject> _listPlanets = new ObservableCollection<SpaceObject>();

        public string Name { get => name; set => name = value; }
        public ObservableCollection<SpaceObject> ListPlanets { get => _listPlanets; set => _listPlanets = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Changed<T>(string p, T value)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        public Solarsystem(string name)
        {
            Name = name;
        }
    }
}
