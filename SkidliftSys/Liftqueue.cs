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

        public Liftqueue(List<Person> people, int amountlift, int timewait)
        {
            occupants = people;
            liftamount = amountlift;
            waittime = timewait;
            running_waittime = timewait;
        }
        
        public void Liftpeople(int timestep) 
        {
            running_waittime -= timestep;    
            if (running_waittime <= 0)       //if the waittime is elapsed we lift people.
            {
                running_waittime += waittime;                   //reset running_waittime
                List<Person> movingpeople = new List<Person>();
                int actuallift;
                if (occupants.Count > liftamount)
                {
                    actuallift = liftamount;
                }
                else
                {
                    actuallift = occupants.Count;
                }

                for(int i = 0; i < actuallift; i++)
                {
                    movingpeople.Add(occupants[i]); //lift always takes from the front of the queue.
                }
                foreach(Location i in possiblemovements)
                {
                    if(i is Lift)
                    {
                        foreach(Person j in movingpeople)
                        {
                            i.MovePerson(j, this);
                        }
                        break;
                    }
                }
            }
        }

        
    }
}
