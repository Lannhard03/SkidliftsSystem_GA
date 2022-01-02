using System;
using System.Collections.Generic;
using System.Text;

namespace InformationalChartsTool
{
    public class LiftQueue : Location
    {
        
        int liftAmount; //how much one lifting lifts
        int waitTime;   //Time between each lifting
        int currentWaitTime; //=waittime;

        public LiftQueue(List<Person> occupants, int liftAmount, int waitTime, string name)
        {
            this.name = name;
            this.occupants = occupants;
            this.liftAmount = liftAmount;
            this.waitTime = waitTime;
            currentWaitTime = 0;
        }

        public LiftQueue(int liftAmount, int waitTime, string name)
        {
            this.name = name;
            this.liftAmount = liftAmount;
            this.waitTime = waitTime;
            currentWaitTime = 0;
        }

        public override void Update(int timeStep) 
        {
            currentWaitTime += timeStep;    
            if (currentWaitTime >= waitTime)   //if the waittime is elapsed we lift people.
            {
                currentWaitTime -= waitTime;    //reset running_waittime
                for(int i = 0; i < liftAmount; i++)
                {
                    if (i >= occupants.Count)
                    {
                        break;
                    }
                    MakeDecision(occupants[i], possibleMovements).MovePerson(occupants[i], this); //lift always takes from the front of the queue.
                }
            }
        }

        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            foreach(Connection c in possibleMovements)
            {
                if (c.leadingTo is LiftQueue)
                {
                    return c.leadingTo;
                }
            }
            return possibleMovements[0].leadingTo;
            
        }
    }
}
