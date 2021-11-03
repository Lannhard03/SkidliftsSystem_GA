using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    public class Lift : Location
    {
        
        int liftamount; //how much one lifting lifts
        int waittime;   //Time between each lifting
        int running_waittime; //=waittime;

        public Lift()
        {

        }
        
        public void Liftpeople(int timestep) //move people from queue to associated lift And if someone is at the top of lift/ at an exit they must/may exit the lift.
        {
            running_waittime -= timestep;    
            if (running_waittime <= 0)       //if the waittime is elapsed we lift people.
            {
                running_waittime += waittime;                   //reset running_waittime
                List<Person> movingpeople = new List<Person>();

                for(int i = 0; i < liftamount; i++)
                {
                    movingpeople.Add(occupants[i]); //lift always takes from the front of the queue.
                }

                possiblemovements[0].AddPeople(movingpeople);      //Maybe the first in the list of movements is the associated queue?
                RemovePeople(movingpeople);                        //Remove the people from this queue
            }
        }


    }
}
