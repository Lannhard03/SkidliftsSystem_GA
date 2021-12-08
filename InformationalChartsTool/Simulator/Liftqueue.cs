using System;
using System.Collections.Generic;
using System.Text;

namespace InformationalChartsTool
{
    public class LiftQueue : Location
    {
        int liftAmount;
        int waitTime;
        int currentWaitTime; 

        public LiftQueue(List<Person> occupants, int liftAmount, int waitTime, string name)
        {
            this.name = name;
            this.occupants = occupants;
            this.liftAmount = liftAmount;
            this.waitTime = waitTime;
            currentWaitTime = waitTime;
        }
        
        public void LiftQueueMove(int timeStep) 
        {
            currentWaitTime -= timeStep;    
            if (currentWaitTime <= 0)   //if the waittime is elapsed we lift people.
            {
                currentWaitTime += waitTime;    //reset running_waittime
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
                foreach(Connection i in possibleMovements)
                {
                    if(i.leadingTo is Lift)
                    {
                        foreach(Person j in movingPeople)
                        {
                            i.leadingTo.MovePerson(j, this);
                        }
                        break;
                    }
                }
            }
        }

        
    }
}
