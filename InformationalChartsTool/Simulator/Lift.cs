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
        public void LiftMove(int timeStep)
        {
            List<Person> movingPeople = new List<Person>();
            foreach (Person i in occupants)
            {
                i.timeLocation += timeStep; 
                if (i.timeLocation >= liftingTime)
                {
                    movingPeople.Add(i);
                }
            }
            movingPeople.Sort((x,y) => y.timeLocation.CompareTo(x.timeLocation));
            //increase the time in the location by timestep and move person if nessecary.
            //note that x,y change places, then we sort from highest to lowest (with respect to time_location)

            foreach (Person i in movingPeople)
            {
                i.DecisionHandler(possibleMovements, this).MovePerson(i, this);
            }
        }
       





    }
}
