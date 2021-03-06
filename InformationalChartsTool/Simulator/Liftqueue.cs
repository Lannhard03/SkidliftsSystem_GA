using System;
using System.Collections.Generic;
using System.Text;

namespace InformationalChartsTool
{
    public class LiftQueue : Location
    {
        int liftAmount; //how much one lifting lifts
        int waitTime;   //Time between each lifting
        int currentWaitTime; //time since last lifting

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
            //for larger timeSteps currentWaiTtime could be much larger than waittim: lift needs to be updated many times
            while (currentWaitTime >= waitTime)
            {
                currentWaitTime -= waitTime; 

                int actuallLiftAmount = occupants.Count > liftAmount ? liftAmount : occupants.Count; //lifts all people or max liftamount
                for(int i = actuallLiftAmount-1; i >= 0; i--)
                {
                    MakeDecision(occupants[i], possibleMovements).MovePerson(occupants[i], this);
                }
            }
        }
        
        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            foreach(Connection c in possibleMovements) //list should only contain one lift
            {
                if (c.leadingTo is Lift)
                {
                    return c.leadingTo;
                }
            }
            return possibleMovements[0].leadingTo;
        }
    }
}
