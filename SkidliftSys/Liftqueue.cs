using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    public class Liftqueue : Location
    {
        
        int liftamount; //how much one lifting lifts
        int waittime;   //Time between each lifting
        int running_waittime; //=waittime;

        public Liftqueue()
        {

        }
        
        public void Liftpeople(int timestep) 
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
                foreach(Location i in possiblemovements)
                {
                    if(i is Lift)
                    {
                        i.AddPeople(movingpeople); //find the first Lift in the list of possiblemovements, there should only be one.
                        break;
                    }
                }
                RemovePeople(movingpeople);//Remove the people from this queue
            }
        }

        
    }
}
