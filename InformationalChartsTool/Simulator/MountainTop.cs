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
            int waittimeMultiplier = 60;

            for (int i = 0; i < occupants.Count; i++)
            {
                occupants[i].timeLocation += timeStep;
                if (occupants[i].chill * waittimeMultiplier < occupants[i].timeLocation)
                {
                    MakeDecision(occupants[i], possibleMovements).MovePerson(occupants[i], this);
                    i--;
                }
                //People will chill for a little while
            }
        }

        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            List<Decision> possibleDecisions = new List<Decision>();
            foreach (Connection c in possibleMovements.Where(x => (
            x.leadingTo is Restaurant 
            || x.leadingTo is Slope
            || x.leadingTo is Home 
            || x.leadingTo is LiftQueue)
            && !x.closed))
            {
                //convert list to decisions and pickout desired locations
                possibleDecisions.Add(new Decision(c.leadingTo, 0));
            }

            foreach (Decision possibleDecision in possibleDecisions)
            {
                //calculate weight of location types
                if (possibleDecision.decision is Slope)
                {
                    possibleDecision.weight += decisionMaker.WeightCuriousness(100, possibleDecision) + decisionMaker.WeightSkillLevel(100, possibleDecision);
                }
                if (possibleDecision.decision is Restaurant)
                {
                    possibleDecision.weight += decisionMaker.WeightHunger(250, possibleDecision); //+ decisionMaker.WeightExplororness(50, possibleDecision);
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

                    possibleDecision.weight += decisionMaker.WeightCuriousness(150, possibleDecision) + decisionMaker.WeightQueueLength(50, possibleDecision, liftOccupants);
                }
            }

            Decision choice = possibleDecisions.OrderByDescending(x => x.weight).First();

            //foreach (Decision d in possibleDecisions)
            //{
            //    Console.WriteLine("From {0} to {1} had {2} weight", this.name, d.decision.name, d.weight);
            //    Console.WriteLine("Occupied {0} times before", decisionMaker.locationHistory.Where(x => x.Item1.Equals(d.decision)).Count());
            //}
            //Console.WriteLine("choice was {0}", choice.decision.name);
            //Console.Write("\n");

            return choice.decision;
        }
    }
}
