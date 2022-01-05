using System;
using System.Collections.Generic;
using System.Linq;

namespace InformationalChartsTool
{
    class Slope : Location
    {
        int slopeTime; //time it takes to ski
        public double difficulty; //1,2,3,4 -> green, blue, red, black

        public Slope(List<Person> occupants, int slopeTime, string name, double difficulty)
        {
            this.name = name;
            this.occupants = occupants;
            this.slopeTime = slopeTime;
            this.difficulty = difficulty;
        }
        public Slope(int slopeTime, string name, double difficulty)
        {   
            this.name = name;
            this.slopeTime = slopeTime;
            this.difficulty = difficulty;
        }

        public override void Update(int timeStep)
        {
            for(int i = 0; i < occupants.Count; i++)
            {
                occupants[i].timeLocation += timeStep;
                if(occupants[i].timeLocation >= slopeTime)
                {
                    MakeDecision(occupants[i], possibleMovements).MovePerson(occupants[i], this);
                }
            }

        }
        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            List<Decision> possibleDecisions = new List<Decision>();
            foreach (Connection c in possibleMovements.Where(x => (x.leadingTo is Slope || x.leadingTo is Valley) && !x.closed))
            {
                //convert list to decisions and pickout desired locations
                possibleDecisions.Add(new Decision(c.leadingTo, 0));
            }

            foreach (Decision possibleDecision in possibleDecisions)
            {
                //calculate weight of location types
                if (possibleDecision.decision is Slope)
                {
                    possibleDecision.weight += decisionMaker.WeightExplororness(100, possibleDecision) + decisionMaker.WeightSkillLevel(100, possibleDecision);
                }
                if (possibleDecision.decision is Valley)
                {
                    possibleDecision.weight += decisionMaker.WeightExplororness(200, possibleDecision) + decisionMaker.WeightTiredness(50, possibleDecision);
                }
            }
            Decision choice = possibleDecisions.OrderByDescending(x => x.weight).First();
            return choice.decision;
        }
    }
}
