using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    class Lift : Location
    {
        //You may exit certainlifts in the middle, then the person makes a decision too contunie (enter the next lift) or leave.
        int timelength;

        public void MoveLift(int timestep)
        {
            List<Person> movingpeople = new List<Person>(); //temp
            foreach (Person i in occupants)
            {
                i.time_location += timestep; //increase the time in the location by timestep and move person if nessecary.
                if (i.time_location >= timelength)
                {
                    i.MakeDecision(possiblemovements);
                }
            }

        }





    }
}
