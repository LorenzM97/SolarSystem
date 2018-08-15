using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Solarsystem
{
    public class Solarsystem
    {
        public string Name;
        List<MovingSpaceObject> _childPlanets = new List<MovingSpaceObject>();

        public Solarsystem(string name)
        {
            Name = name;
        }

    }
}
