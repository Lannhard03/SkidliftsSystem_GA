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
            //note that x,y change places, then we sort from highest to lowest (with respect to timeLocation)

            foreach (Person p in movingPeople)
            {
                MakeDecision(p, possibleMovements).MovePerson(p, this);
            }
        }
        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            //Look for MountainTop or Slopes and Lifts (we may jump off lift in middle)

            Connection temp = possibleMovements.Find(x => x.leadingTo is MountainTop && !x.closed);
            if (temp != null)
            {
                return temp.leadingTo;
            }
            //If there is a mountainTop go there.

            //how to choose if to go up or jump of??
            //Well going up will in all liklyhood lead to more choices and possibly going to restaurant?
            //And jumping of will mean you can do what? Primarily we will go up?

            temp = possibleMovements.Find(x => x.leadingTo is Lift && !x.closed);
            if (temp != null)
            {
                return temp.leadingTo;
            }
            //If there is a Lift go there.

            return possibleMovements[0].leadingTo;
           





        }
    }
}
