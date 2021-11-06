using System;
using System.Collections.Generic;
using System.Linq;

namespace SkidliftSys
{
    class Slope : Location
    {
        
        int totalslopetime;
        
        public Slope()
        {

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
                i.DecisionHandler(possiblemovements, this).AddPerson(i);
                RemovePerson(i);
            }
        }


        

    }
}
