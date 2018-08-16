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
        public string Name;
        public ObservableCollection<SpaceObject> _listPlanets = new ObservableCollection<SpaceObject>();

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
