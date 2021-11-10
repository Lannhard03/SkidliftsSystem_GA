using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    class Restaurant : Location
    {
        public int maxOccupants;

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
