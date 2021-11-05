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
            possiblemovements[0].AddPeople(movingpeople);      //Maybe the first in the list of movements is the associated queue?
            RemovePeople(movingpeople);                        //Remove the people from this queue

        }


        

    }
}
