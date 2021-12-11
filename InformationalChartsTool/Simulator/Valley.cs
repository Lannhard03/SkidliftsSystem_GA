using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace InformationalChartsTool
{
    class Valley : Location
    {
        public override void Update(int timstep)
        {
            
            for(int i = 0; i<occupants.Count; i++)
            {
                MakeDecision(occupants[i], possibleMovements).MovePerson(occupants[i], this);
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
            //Look for Restaurant, Home, Slopes since MountainTop should have no Liftqueues or Vallys adjacent

            foreach (Decision possibleDecision in possibleDecisions)
            {
                if (possibleDecision.decision is LiftQueue)
                {
                    possibleDecision.weight += decisionMaker.WeightExplororness(200, possibleDecision);
                }
                if (possibleDecision.decision is Restaurant)
                {
                    possibleDecision.weight += decisionMaker.WeightHunger(200, possibleDecision) + decisionMaker.WeightExplororness(50, possibleDecision);
                }
                if (possibleDecision.decision is Home)
                {
                    possibleDecision.weight += decisionMaker.WeightTiredness(200, possibleDecision) + decisionMaker.WeightHunger(50, possibleDecision);
                }
            }

            //Determined way, largest weight wins.
            Decision choice = possibleDecisions.OrderByDescending(x => x.weight).First();
            return choice.decision;

        }

    }
}
