using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    public abstract class Location
    {
        public List<Person> occupants = new List<Person>();
        public List<Location> possiblemovements = new List<Location>(); //How to express location? Absolute location might be useful for pathfinding but how do you express it? Relative position probably enough?
        
        public virtual void MovePerson(Person people, Location comingfrom)
        {
            AddPerson(people);
            comingfrom.RemovePerson(people);
        }
        public virtual void RemovePerson(Person people)
        {
            occupants.Remove(people);
        }

        public virtual void AddPerson(Person people)
        {
            occupants.Add(people);
            people.time_location = 0;
        }

    }
}
