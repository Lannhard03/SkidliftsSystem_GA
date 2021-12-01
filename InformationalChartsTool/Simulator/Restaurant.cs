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
        public void RestaurantMove(int timeStep) 
        {
            foreach(Person i in occupants)
            {
                i.timeLocation += timeStep;
            }

            foreach (Person i in occupants)
            {
                i.DecisionHandler(possibleMovements, this).MovePerson(i, this); //Person i makes a decision and moves there.
            }
        }

        public override void MovePerson(Person person, Location comingFrom)
        {
            if (occupants.Count > maxOccupants)
            {
                //if the restaurant is full we can't let someone enter it.
                possibleMovements.Remove(Connection.GetIndexOfLocation(possibleMovements, this));
                person.DecisionHandler(possibleMovements, comingFrom).MovePerson(person, comingFrom); //Person i makes a decision and moves there.
            }
            else
            {
                base.MovePerson(person, comingFrom);
            }
        }
    }
}
