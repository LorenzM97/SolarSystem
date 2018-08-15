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
        List<MovingSpaceObject> _childMoons = new List<MovingSpaceObject>();

    }
}