using System;
using System.Collections.Generic;
using System.Linq;

namespace InformationalChartsTool
{
    class Lift : Location
    {
        //You may exit certain lifts in the middle, then the person makes a decision to continue (enter the next lift) or leave.
        int liftingTime;
        int maxoccupants; //if lift has this many people, don't add more.
        public Lift(List<Person> occupants, int liftingTime, string name)
        {
            this.name = name;
            this.occupants = occupants;
            this.liftingTime = liftingTime;
        }
        public Lift(int liftingTime, string name)
        {
            this.name = name;
            this.liftingTime = liftingTime;
        }
        public override void Update(int timeStep)
        {
            List<Person> movingPeople = new List<Person>();
            foreach (Person p in occupants)
            {
                p.timeLocation += timeStep; 
                if (p.timeLocation >= liftingTime)
                {
                    movingPeople.Add(p);
                }
            }
            movingPeople.Sort((x,y) => y.timeLocation.CompareTo(x.timeLocation));
            //increase the time in the location by timestep and move person if nessecary.
            //note that x,y change places, then we sort from highest to lowest (with respect to time_location)

            foreach (Person p in movingPeople)
            {
                
                this.Decision(p, possibleMovements).MovePerson(p, this);
            }
        }
        public override Location Decision(Person decisionMaker, List<Connection> possibleMovements)
        {
            //Disregarding top of mountain location for now we can say that the person will look for the first slopes in the list and pick one.


            List<Slope> possibleSlopes = new List<Slope>();
            foreach (Connection i in possibleMovements)
            {
                if (i.leadingTo is Slope slope && !i.closed)
                {
                    possibleSlopes.Add(slope);
                }
            }
            Random rnd = new Random();
            return (possibleSlopes[0]); //Basic behaivour, pick a random slope.
        } //This is definiatlly not finished!! will chrash with mountaintop
    }
}
