using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    class Lift
    {
        List<Person> occupants = new List<Person>();
        int liftamount; //how much one lifting lifts
        int waittime; //Time between each lifting


        int running_waittime; //=waittime;

        List<Connections> possiblemovements = new List<Connections>(); //How to express location? Absolute location might be useful for pathfinding but how do you express it? Relative position probably enough?
        
        public void Liftpeople(int timestep) //move people from queue to associated lift And if someone is at the top of lift/ at an exit they must/may exit the lift.
        {
            running_waittime -= timestep; //the time left to wait by the timestep we are taking
            if (running_waittime <= 0) //if the waittime is elapsed we lift people.
            {
                running_waittime += waittime; //reset running_waittime
                List<Person> movingpeople = new List<Person>();

                for(int i = 0; i < liftamount; i++)
                {
                    movingpeople.Add(occupants[i]); //lift always takes from the front of the queue.
                }

                possiblemovements[0].end.Addpeople(movingpeople);      //Maybe the first in the list of movements is the associated queue?
                RemovePeople(movingpeople);
            }
        }

        public void RemovePeople(List<Person> people)
        {
            foreach(Person i in people)
            {
                occupants.Remove(i);
            }
        }

        public void AddPeople(List<Person> people)
        {
            foreach (Person i in people)
            {
                occupants.Add(i);
            }
        }
    }
}
