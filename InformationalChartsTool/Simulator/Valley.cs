using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace InformationalChartsTool
{
    class Valley : Location
    {
        public Valley(string name)
        {
            this.name = name;
        }

        public override void Update(int timeStep)
        {
            int waittimeMultiplier = 120; //maximum time spent

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

        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            List<Decision> possibleDecisions = new List<Decision>();
            foreach (Connection c in possibleMovements.Where(x => (x.leadingTo is Restaurant || x.leadingTo is LiftQueue|| x.leadingTo is Home || x.leadingTo is Slope) && !x.closed))
            {
                //convert list to decisions and pickout desired locations
                possibleDecisions.Add(new Decision(c.leadingTo, 0));
            }

            foreach (Decision possibleDecision in possibleDecisions)
            {
                //calculate weight of location types
                if (possibleDecision.decision is LiftQueue)
                {
                    int liftOccupants = 0; //total occupants of all adjacent lifts
                    foreach(Decision d in possibleDecisions)
                    {
                        if (d.decision is LiftQueue)
                        {
                            liftOccupants += d.decision.occupants.Count;
                        } 
                    }

                    possibleDecision.weight += decisionMaker.WeightExplororness(50, possibleDecision)+decisionMaker.WeightQueueLenght(150, possibleDecision, liftOccupants);
                }
                if (possibleDecision.decision is Restaurant)
                {
                    possibleDecision.weight += decisionMaker.WeightHunger(200, possibleDecision); //+ decisionMaker.WeightExplororness(50, possibleDecision);
                }
                if (possibleDecision.decision is Home)
                {
                    possibleDecision.weight += decisionMaker.WeightTiredness(100, possibleDecision) + decisionMaker.WeightHunger(100, possibleDecision);
                }
                if (possibleDecision.decision is Slope)
                {
                    possibleDecision.weight += decisionMaker.WeightExplororness(50, possibleDecision) + decisionMaker.WeightSkillLevel(75, possibleDecision);
                }
            }

            //Determined way, largest weight wins.
            Decision choice = possibleDecisions.OrderByDescending(x => x.weight).First();

            return choice.decision;
        }
    }
}
