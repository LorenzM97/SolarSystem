using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ClassLibrary
{
    public class SpaceObject : INotifyPropertyChanged
    {
        public int X;
        public int Y;
        public int Size;
        public int Distance;
        private string _name;

        public string Name
        {
            get => _name;
            set { _name = value; Changed<string>("Name", value); } 
        }

       
        public string  Type;
        public double Degree;
        public ObservableCollection<SpaceObject> _listMoons = new ObservableCollection<SpaceObject>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void Changed<T>(string p, T value)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
   
        }

        public SpaceObject(string type, string name, int size, int distance, double degree)
        {
            Type = type;
            Degree = degree;
            Size = size;
            Distance = distance;
            Name = name;
        }

        public SpaceObject()
        {

        }

        public SpaceObject(string type, string name, int x, int y, int size)
        {
            Type = type;
            X = x;
            Y = y;
            Size = size;
            Name = name;
        }
    }
}