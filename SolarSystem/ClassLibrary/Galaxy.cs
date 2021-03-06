﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

//awesmoe comment header needed

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
            if (PropertyChanged != null) //if the changed Property is not null, the method will be called
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        protected void Changed2(string p)
        {
            if (PropertyChanged != null) //if the changed Property is not null, the method will be called
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
    }
}
