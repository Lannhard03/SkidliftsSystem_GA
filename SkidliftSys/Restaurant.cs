using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    class Restaurant : Location
    {
        public int Maxoccupants { get; set; }

        public override void MovePerson(Person people, Location comingfrom)
        {
            if (occupants.Count > Maxoccupants)
            {
                //if the restaurant is full we can't let someone enter it.
                List<Location> temppossiblemovements = new List<Location>();
                foreach(Location i in comingfrom.possiblemovements)
                {
                    temppossiblemovements.Add(i);
                }
                temppossiblemovements.Remove(this); //creates a new list of possiblemovements with the full restaurant removed.
                people.DecisionHandler(temppossiblemovements, comingfrom).MovePerson(people, comingfrom);
            }
            else
            {
                base.MovePerson(people, comingfrom);
            }

            
        }
    }
}
