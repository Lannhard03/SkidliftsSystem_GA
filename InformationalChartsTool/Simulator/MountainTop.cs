using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace InformationalChartsTool
{
    class MountainTop : Location
    {
        public MountainTop(string name)
        {
            this.name = name;
        }

        public override void Update(int timeStep)
        {
            int waittimeMultiplier = 60; //maximum waittime of location

            for (int i = 0; i < occupants.Count; i++)
            {
                occupants[i].timeLocation += timeStep;
                if (occupants[i].chill * waittimeMultiplier < occupants[i].timeLocation)
                {
                    MakeDecision(occupants[i], possibleMovements).MovePerson(occupants[i], this);
                    i--;
                }
            }
        }

        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            //Filter possibleMovements for desireable Locations while converting connections to decision
            List<Decision> possibleDecisions = new List<Decision>();
            foreach (Connection c in possibleMovements.Where(x =>
            (
            x.leadingTo is Restaurant
            || x.leadingTo is Slope
            || x.leadingTo is Home
            || x.leadingTo is LiftQueue
            )
            && !x.closed))
            {
                possibleDecisions.Add(new Decision(c.leadingTo, 0));
            }

            foreach (Decision possibleDecision in possibleDecisions)
            {
                //calculate weight of location types
                if (possibleDecision.decision is Slope)
                {
                    possibleDecision.weight += decisionMaker.WeightExplororness(100, possibleDecision) + decisionMaker.WeightSkillLevel(100, possibleDecision);
                }
                if (possibleDecision.decision is Restaurant)
                {
                    possibleDecision.weight += decisionMaker.WeightHunger(250, possibleDecision);
                }
                if (possibleDecision.decision is Home)
                {
                    possibleDecision.weight += decisionMaker.WeightTiredness(200, possibleDecision);
                }
                if (possibleDecision.decision is LiftQueue)
                {
                    int liftOccupants = 0; //total occupants of all adjacent lifts
                    foreach (Decision d in possibleDecisions)
                    {
                        if (d.decision is LiftQueue)
                        {
                            liftOccupants += d.decision.occupants.Count;
                        }
                    }

                    possibleDecision.weight += decisionMaker.WeightExplororness(150, possibleDecision) + decisionMaker.WeightQueueLenght(50, possibleDecision, liftOccupants);
                }
            }

            //pick decision with largest weight
            Decision choice = possibleDecisions.OrderByDescending(x => x.weight).First();
            return choice.decision;
        }
    }
}
