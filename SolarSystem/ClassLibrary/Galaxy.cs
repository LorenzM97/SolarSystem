using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Library_Solarsystem
{
    public class Galaxy : INotifyPropertyChanged
    {
        //public string Name;
        private ObservableCollection<Solarsystem> listSystems = new ObservableCollection<Solarsystem>();

        public ObservableCollection<Solarsystem> ListSystems { get => listSystems; set => listSystems = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Changed<T>(string p, T value)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
    }
}
