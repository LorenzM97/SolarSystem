using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Library_Solarsystem
{
    public class Galaxy
    {
        public string Name;

        public ObservableCollection<Solarsystem> _listSystems = new ObservableCollection<Solarsystem>();

       
    }
}
