using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace InformationalChartsTool
{
    class Valley : Location
    {
        public override void Update(int timeStep)
        {

            int waittimeMultiplier = 120;

            for (int i = 0; i < occupants.Count; i++)
            {
                occupants[i].timeLocation += timeStep;
                if (occupants[i].chill * waittimeMultiplier < occupants[i].timeLocation)
                {
                    MakeDecision(occupants[i], possibleMovements).MovePerson(occupants[i], this);
                }
                    //People will chill for a little while
                    //Class may seem unnessecary but it will govern what type of choices Persons make
            }
        }

        public Valley(string name)
        {
            this.name = name;
        }

        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            List<Decision> possibleDecisions = new List<Decision>();
            foreach (Connection c in possibleMovements.Where(x => x.leadingTo is Restaurant || x.leadingTo is LiftQueue|| x.leadingTo is Home))
            {
                if (!c.closed)
                {
                    possibleDecisions.Add(new Decision(c.leadingTo, 0));
                }
            }
            //Look for Restaurant, Home, Slopes since MountainTop should have no Liftqueues or Valleys adjacent

            foreach (Decision possibleDecision in possibleDecisions)
            {
                if (possibleDecision.decision is LiftQueue)
                {
                    int liftOccupants = 0;
                    foreach(Decision d in possibleDecisions)
                    {
                        if (d.decision is LiftQueue)
                        {
                            liftOccupants += d.decision.occupants.Count;
                        } 
                    }
                    //Console.WriteLine("total queue count: {0}", liftOccupants);

                    possibleDecision.weight += decisionMaker.WeightExplororness(50, possibleDecision)+decisionMaker.WeightQueueLenght(150, possibleDecision, liftOccupants);
                }
                if (possibleDecision.decision is Restaurant)
                {
                    possibleDecision.weight += decisionMaker.WeightHunger(150, possibleDecision) + decisionMaker.WeightExplororness(50, possibleDecision);
                }
                if (possibleDecision.decision is Home)
                {
                    possibleDecision.weight += decisionMaker.WeightTiredness(200, possibleDecision) + decisionMaker.WeightHunger(50, possibleDecision);
                }
            }

            //Determined way, largest weight wins.
            Decision choice = possibleDecisions.OrderByDescending(x => x.weight).First();

            //foreach (Decision d in possibleDecisions)
            //{
            //    Console.WriteLine("From {0} to {1} had {2} weight", this.name, d.decision.name, d.weight);
            //    Console.WriteLine("QueueLength: {0}", d.decision.occupants.Count);
            //    Console.WriteLine("Occupied {0} times before", decisionMaker.locationHistory.Where(x => x.Item1.Equals(d.decision)).Count());
            //}
            //Console.WriteLine("choice was {0}", choice.decision.name);
            //Console.Write("\n");
            return choice.decision;

        }

    }
}
