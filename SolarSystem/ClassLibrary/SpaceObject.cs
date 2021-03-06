﻿using Library_Solarsystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Library_Solarsystem
{
    public class SpaceObject : Solarsystem, INotifyPropertyChanged
    {
        public int X;
        public int Y;
        private int size;
        private int distance;
        private double degree;
        public string Type;
        public SpaceObject Parent;
        //        {
        //            get => Distance;
        //            set { Distance = value; Changed<int>("Distance", value); }
        //}



        public ObservableCollection<SpaceObject> ListMoons { get => listMoons; set => listMoons = value; }
        public int Distance { get => distance; set { distance = value; Changed2("Distance"); } }
        public double Degree { get => degree; set { degree = value; Changed2("Degree"); } }

        public int Size { get => size; set { size = value; Changed2("Size"); } }

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

       
        public SpaceObject(string type, string name, int size, int distance, double degree) : base (name)       //Planet
        {
            Type = type;
            Degree = degree;
            Size = size;
            Distance = distance;
            Name = name;
        }

        public SpaceObject(string type, string name, SpaceObject parent, int size, int distance, double degree) : base(name)       //Moon
        {
            Type = type;
            Degree = degree;
            Size = size;
            Distance = distance;
            Name = name;
            Parent = parent;
        }


        public SpaceObject(string type, string name, int x, int y, int size) : base(name)       //Sun
        {
            Type = type;
            X = x;
            Y = y;
            Size = size;
        }

        public SpaceObject(int x, int y,string type, string name, int size)      //Sun
        {
            X = x;
            Y = y;
            Type = type;
            Name = name;
            Size = size;
        }

        public void Move(float speed, int middleWidth, int middleHeight)
        {
            this.X = (int)((Distance * Math.Cos(Degree)) + middleWidth);
            this.Y = (int)((Distance * Math.Sin(Degree)) + middleHeight);

            Degree = (Degree + (float)Math.PI / 100 * (speed / 2));           
        }

        public void Move(float speed)
        {
            this.X = (int)((Distance * Math.Cos(Degree)) + Parent.X);
            this.Y = (int)((Distance * Math.Sin(Degree)) + Parent.Y);

            Degree = (Degree - (float)Math.PI / 100 * (speed / 2));
        }


    }
}