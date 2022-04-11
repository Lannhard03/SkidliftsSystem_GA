using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InformationalChartsTool
{
    class Restaurant : Location
    {
        public int maxOccupants;

        //for debugging
        public List<double> hungers = new List<double>();

        public Restaurant(int maxOccupants, string name)
        {
            this.maxOccupants = maxOccupants;
            this.name = name;
        }

        public override void Update(int timeStep)
        {
            //time to spend in location
            int eatingTimeMultiple = 1800;
            int eatingTimeBase = 900;

            for (int i = 0; i < occupants.Count; i++)
            {
                occupants[i].timeLocation += timeStep;
                if (eatingTimeMultiple * occupants[i].chill + eatingTimeBase <= occupants[i].timeLocation)
                {
                    occupants[i].hunger = -2; //Person should only eat once
                    MakeDecision(occupants[i], possibleMovements).MovePerson(occupants[i], this);
                    i--;
                }
            }
        }

        public override void MovePerson(Person person, Location comingFrom)
        {
            //if the restaurant is full we can't let someone enter it.
            if (occupants.Count > maxOccupants)
            {
                comingFrom.possibleMovements.Find(x => x.leadingTo == this).closed = true; 
                comingFrom.MakeDecision(person, comingFrom.possibleMovements).MovePerson(person, comingFrom); //person makes new decision
            }
            else
            {
                base.MovePerson(person, comingFrom);
                //for debugging
                hungers.Add(person.hunger);
            }
        }
    }
}
