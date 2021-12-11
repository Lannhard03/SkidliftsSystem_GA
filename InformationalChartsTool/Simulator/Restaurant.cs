using System;
using System.Collections.Generic;
using System.Text;

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
            foreach(Person i in occupants)
            {
                i.timeLocation += timeStep;
            }

            foreach (Person i in occupants)
            {
                //Person i makes a decision and moves there.
                this.MakeDecision(i, possibleMovements).MovePerson(i, this);
            }
        }

        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            return possibleMovements[0].leadingTo;
        }

        public override void MovePerson(Person person, Location comingFrom)
        {
            if (occupants.Count > maxOccupants)
            {
                //if the restaurant is full we can't let someone enter it.
                comingFrom.possibleMovements[Connection.GetIndexOfLocation(possibleMovements, this)].closed = true;
                comingFrom.MakeDecision(person, comingFrom.possibleMovements).MovePerson(person, comingFrom);
                

            }
            else
            {
                base.MovePerson(person, comingFrom);
            }
        }
    }
}
