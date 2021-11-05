using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    public abstract class Location
    {
        public List<Person> occupants = new List<Person>();
        public List<Location> possiblemovements = new List<Location>(); //How to express location? Absolute location might be useful for pathfinding but how do you express it? Relative position probably enough?
        
        public virtual void RemovePeople(List<Person> people)
        {
            foreach (Person i in people)
            {
                occupants.Remove(i);
                i.time_location = 0;
            }
        }

        public virtual void AddPeople(List<Person> people)
        {
            foreach (Person i in people)
            {
                occupants.Add(i);
                i.time_location = 0;
            }
        }

    }
}
