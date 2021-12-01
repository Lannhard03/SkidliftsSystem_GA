using System;
using System.Collections.Generic;
using System.Text;

namespace InformationalChartsTool
{
    class MountainTop : Location
    {
        //Many Lifts may lead to this place, and people may spend some time waiting here.

        public void TopOfMountainMove(int timeStep)
        {
            foreach(Person i in occupants)
            {
                i.DecisionHandler(possibleMovements, this).MovePerson(i, this);
                //Class may seem unnessecary but it will govern what type of choices Persons make
            }
        }


    }
}
