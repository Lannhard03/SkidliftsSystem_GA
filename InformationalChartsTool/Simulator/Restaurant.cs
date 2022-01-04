using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InformationalChartsTool
{
    class Restaurant : Location
    {
        public int maxOccupants;

        public Restaurant(List<Person> occupants, int maxOccupants, string name)
        {
            this.occupants = occupants;
            this.maxOccupants = maxOccupants;
            this.name = name;
        }
        public Restaurant(int maxOccupants, string name)
        {
            this.maxOccupants = maxOccupants;
            this.name = name;
        }

        //remember that this is movement away from restaurant and that MovePerson overide affects incoming people
        public override void Update(int timeStep) 
        {
            int eatingTimeMultiple = 1800;
            int eatingTimeBase = 900;
            for(int i = 0; i<occupants.Count; i++)
            {
                occupants[i].timeLocation += timeStep;
                if(eatingTimeMultiple*occupants[i].chill+eatingTimeBase <= occupants[i].timeLocation)
                {
                    occupants[i].hunger = 0;
                    MakeDecision(occupants[i], possibleMovements).MovePerson(occupants[i], this);
                }
            }
        }

        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            return possibleMovements[0].leadingTo;
        }

        public override void MovePerson(Person person, Location comingFrom) //if the restaurant is full we can't let someone enter it.
        {
            if (occupants.Count > maxOccupants)
            {
                comingFrom.possibleMovements.Find(x => x.leadingTo == this).closed = true;
                //Console.WriteLine("Closed Restaurant");
                comingFrom.MakeDecision(person, comingFrom.possibleMovements).MovePerson(person, comingFrom);
            }
            else
            {
                base.MovePerson(person, comingFrom);
            }
        }
    }
}
