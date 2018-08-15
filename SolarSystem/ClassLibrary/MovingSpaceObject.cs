using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class MovingSpaceObject
    {
        public int X;
        public int Y;
        public int Size;
        public int Distance;
        public string Name, Type;
        List<MovingSpaceObject> _childMoons = new List<MovingSpaceObject>();

        public MovingSpaceObject(string type, string name, int x, int y, int size, int distance)
        {
            Type = type;
            X = x;
            Y = y;
            Size = size;
            Distance = distance;
            Name = name;
        }

        public MovingSpaceObject(string type, string name, int x, int y, int size)
        {
            Type = type;
            X = x;
            Y = y;
            Size = size;
            Distance = 0;
            Name = name;
        }
    }
}