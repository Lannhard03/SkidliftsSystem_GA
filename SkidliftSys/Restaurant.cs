using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    class Restaurant : Location
    {
        public int maxOccupants;

        public override void MovePerson(Person people, Location comingFrom)
        {
            if (occupants.Count > maxOccupants)
            {
                //if the restaurant is full we can't let someone enter it.
                //this solution is bad; Perhaps connections can be turned off or the decision code will disallow the bad kind of movement

                List<Location> currentPossibleMovements = new List<Location>();
                foreach(Location i in comingFrom.possibleMovements)
                {
                    currentPossibleMovements.Add(i);
                }
                currentPossibleMovements.Remove(this); //creates a new list of possiblemovements with the full restaurant removed.
                people.DecisionHandler(currentPossibleMovements, comingFrom).MovePerson(people, comingFrom);
            }
            else
            {
                base.MovePerson(people, comingFrom);
            }

            
        }
    }
}
