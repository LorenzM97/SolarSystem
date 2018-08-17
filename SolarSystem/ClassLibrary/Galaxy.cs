using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Library_Solarsystem
{
    public class Galaxy
    {
        public string Name;

         ObservableCollection<Solarsystem> _listSystems = new ObservableCollection<Solarsystem>();

        public ObservableCollection<Solarsystem> ListSystems { get => _listSystems; set => _listSystems = value; }
    } 
}
