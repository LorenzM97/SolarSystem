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
        private int distance;
        private double degree;
        public string Type;
        public SpaceObject _parent;



        public ObservableCollection<SpaceObject> ListMoons { get => listMoons; set => listMoons = value; }
        public int Distance { get => distance; set { distance = value; Changed2("Distance"); } }
        public double Degree { get => degree; set { degree = value; Changed2("Degree"); } }

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

        public void Move(float speed, int middleWidth, int middleHeight)
        {
            this.X = (int)((this.Distance * Math.Cos(Degree)) + middleWidth);
            this.Y = (int)((this.Distance * Math.Sin(Degree)) + middleHeight);

            Degree = (Degree + (float)Math.PI / 100 * speed);

            foreach(var item in ListMoons)
            {
                item.X = (int)((item.Distance * Math.Cos(item.Degree)) + this.X + this.Size / 3);
                item.Y = (int)((item.Distance * Math.Sin(item.Degree)) + this.Y + this.Size / 3);

                item.Degree = (item.Degree + (float)Math.PI / 100 * speed);
            }
        }
    }
}