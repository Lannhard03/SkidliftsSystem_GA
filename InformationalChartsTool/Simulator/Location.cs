using System;
using System.Collections.Generic;
using System.Text;

namespace InformationalChartsTool
{
    public abstract class Location
    {
        public string name;
        public List<Person> occupants = new List<Person>();
        public List<Connection> possibleMovements = new List<Connection>();
        public List<int> timeBasedOccupantCounts = new List<int>();
        
        public virtual void MovePerson(Person person, Location comingFrom) //Take Person coming from a location and puts it in the one calling the method
        {
            AddPerson(person);
            comingFrom.RemovePerson(person);
            person.locationHistory.Add(Tuple.Create(comingFrom,Simulation.time)); //for output
        }
        public virtual void RemovePerson(Person people)
        {
            occupants.Remove(people);
        }
        public virtual void AddPerson(Person people)
        {
            occupants.Add(people);
            people.timeLocation = 0; //reset time
        }
        public virtual void Update(int timeStep)
        {
            //overidden in derived locations
        }
        public virtual Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            return possibleMovements[0].leadingTo;
        }
    }
}
