using Library_Solarsystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ClassLibrary
{
    public class SpaceObject : Solarsystem, INotifyPropertyChanged
    {
        public int X;
        public int Y;
        public int Size;
        public int Distance;
//        {
//            get => Distance;
//            set { Distance = value; Changed<int>("Distance", value); }
//}
private string _name;

        //public string Name
        //{
        //    get => name;
        //    set { name = value; Changed<string>("Name", value); } 
        //}

        public ObservableCollection<SpaceObject> ListMoons { get => listMoons; set => listMoons = value; }

        public string  Type;
        public double Degree;
        private ObservableCollection<SpaceObject> listMoons = new ObservableCollection<SpaceObject>();

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void Changed<T>(string p, T value)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(p));

        //}

        public SpaceObject()
        {

        }

        public SpaceObject(string name) : base(name)
        {
        }

        public SpaceObject(string type, string name, int size, int distance, double degree) : base (name)
        {
            Type = type;
            Degree = degree;
            Size = size;
            Distance = distance;
            //Name = name;
        }

        public SpaceObject(string type, string name, int x, int y, int size) : base(name)
        {
            Type = type;
            X = x;
            Y = y;
            Size = size;
            //Name = name;
        }
    }
}