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
        public double Degree;
        public string Type;
        public SpaceObject _parent;
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

        public SpaceObject(string type, string name, SpaceObject parent, int size, int distance, double degree)
        public SpaceObject(string name) : base(name)
        {
        }

        public SpaceObject(string type, string name, int size, int distance, double degree) : base (name)
        {
            Type = type;
            Degree = degree;
            Size = size;
            Distance = distance;
            Name = name;
            _parent = parent;
        }

        public SpaceObject()
        {

        }

        public SpaceObject(string type, string name, int x, int y, int size) : base(name)
        {
            Type = type;
            X = x;
            Y = y;
            Size = size;
            //Name = name;
        }

        public void Move(float speed)
        {
            this.X = (int)((this.Distance * Math.Cos(Degree)) + _parent.X + _parent.Size / 3);
            this.Y = (int)((this.Distance * Math.Sin(Degree)) + _parent.Y + _parent.Size / 3);

            Degree = (Degree + (float)Math.PI / 100 * speed);
        }
    }
}