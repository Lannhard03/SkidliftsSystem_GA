using System;
using System.Collections.Generic;
using System.Linq;

namespace SkidliftSys
{
    class Slope : Location
    {
        
        int totalslopetime;
        
        public Slope(List<Person> people, int timetoslope)
        {
            occupants = people;
            totalslopetime = timetoslope;
        }
        public Slope(int timetoslope)
        {
            totalslopetime = timetoslope;
        }

        public void SlopeMovement(int timestep)
        {
            List<Person> movingpeople = new List<Person>(); //temp
            foreach(Person i in occupants)
            {
                i.time_location += timestep; //increase the time in the location by timestep and move person if nessecary.
                if (i.time_location >= totalslopetime)
                {
                    movingpeople.Add(i);
                }
            }
            foreach(Person i in movingpeople)
            {
                i.DecisionHandler(possiblemovements, this).MovePerson(i, this); //Person i makes a decision and moves there.
            }
        }


        

    }
}
