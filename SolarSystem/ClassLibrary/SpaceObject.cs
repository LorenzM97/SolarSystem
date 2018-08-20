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

       
        public SpaceObject(string type, string name, int size, int distance, double degree) : base (name)       //Planet, Moon
        {
            Type = type;
            Degree = degree;
            Size = size;
            Distance = distance;
            Name = name;
        }


        public SpaceObject(string type, string name, int x, int y, int size) : base(name)       //Sun
        {
            Type = type;
            X = x;
            Y = y;
            Size = size;
        }

        public SpaceObject(string type, string name, int size)      //Sun
        {
            Type = type;
            Name = name;
            Size = size;
        }

        public void Move(float speed)
        {
            this.X = (int)((this.Distance * Math.Cos(Degree)) + _parent.X + _parent.Size / 3);
            this.Y = (int)((this.Distance * Math.Sin(Degree)) + _parent.Y + _parent.Size / 3);

            Degree = (Degree + (float)Math.PI / 100 * speed);
        }

        public void Move(float speed, List<SpaceObject> list)
        {
            foreach(var item in list)
            {
                item.X = (int)((item.Distance * Math.Cos(item.Degree)) + this.X + this.Size / 3);
                item.Y = (int)((item.Distance * Math.Sin(item.Degree)) + this.Y + this.Size / 3);

                item.Degree = (item.Degree + (float)Math.PI / 100 * speed);

            }
        }

    }
}