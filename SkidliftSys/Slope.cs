using System;
using System.Collections.Generic;
using System.Linq;

namespace SkidliftSys
{
    class Slope : Location
    {
        
        int slopeTime;
        int difficulty;
        public Slope(List<Person> occupants, int slopeTime, string name)
        {
            this.name = name;
            this.occupants = occupants;
            this.slopeTime = slopeTime;
        }
        public Slope(int slopeTime, string name)
        {
            this.name = name;
            this.slopeTime = slopeTime;
        }

        public void SlopeMovement(int timeStep)
        {
            List<Person> movingPeople = new List<Person>(); //temp
            foreach(Person i in occupants)
            {
                i.timeLocation += timeStep; //increase the time in the location by timestep and move person if nessecary.
                if (i.timeLocation >= slopeTime)
                {
                    movingPeople.Add(i);
                }
            }
            foreach(Person i in movingPeople) //why are there two foreach loops (and a temporary list) here when one is enough?
            {
                i.DecisionHandler(possibleMovements, this).MovePerson(i, this); //Person i makes a decision and moves there.
            }

            
        }


        

    }
}
