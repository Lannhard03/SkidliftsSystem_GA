using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    public abstract class Location
    {
        public string name;
        public List<Person> occupants = new List<Person>();
        public List<Connection> possibleMovements = new List<Connection>(); //How to express location? Absolute location might be useful for pathfinding but how do you express it? Relative position probably enough?
        
        
        public virtual void MovePerson(Person person, Location comingFrom) //Take Person coming from a location and puts it in the one calling the method
        {
            AddPerson(person);
            comingFrom.RemovePerson(person);
            person.locationHistory.Add(comingFrom);
        }
        public virtual void RemovePerson(Person people)
        {
            occupants.Remove(people);
        }

        public virtual void AddPerson(Person people)
        {
            occupants.Add(people);
            people.timeLocation = 0;
            
        }


    }
}
