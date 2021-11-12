using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    public class Liftqueue : Location
    {
        
        int liftAmount; //how much one lifting lifts
        int waitTime;   //Time between each lifting
        int currentWaitTime; //=waittime;

        public Liftqueue(List<Person> occupants, int liftAmount, int waitTime, string name)
        {
            this.name = name;
            this.occupants = occupants;
            this.liftAmount = liftAmount;
            this.waitTime = waitTime;
            currentWaitTime = waitTime;
        }
        
        public void Liftpeople(int timeStep) 
        {
            currentWaitTime -= timeStep;    
            if (currentWaitTime <= 0)       //if the waittime is elapsed we lift people.
            {
                currentWaitTime += waitTime;                   //reset running_waittime
                List<Person> movingPeople = new List<Person>();
                int actualLiftAmount;
                if (occupants.Count > liftAmount)
                {
                    actualLiftAmount = liftAmount;
                }
                else
                {
                    actualLiftAmount = occupants.Count;
                }

                for(int i = 0; i < actualLiftAmount; i++)
                {
                    movingPeople.Add(occupants[i]); //lift always takes from the front of the queue.
                }
                foreach(Location i in possibleMovements)
                {
                    if(i is Lift)
                    {
                        foreach(Person j in movingPeople)
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
