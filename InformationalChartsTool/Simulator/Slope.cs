using System;
using System.Collections.Generic;
using System.Linq;

namespace InformationalChartsTool
{
    class Slope : Location
    {
        
        int slopeTime;
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
            List<Person> movingPeople = new List<Person>(); //temp
            foreach(Person i in occupants)
            {
                i.timeLocation += timeStep; //increase the time in the location by timestep and move person if nessecary.
                if (i.timeLocation >= slopeTime)
                {
                    movingPeople.Add(i);
                }
            }
            foreach(Person i in movingPeople) //why are there two foreach loops (and a temporary list) here when one is enough?
            {
                MakeDecision(i, possibleMovements).MovePerson(i, this);
            }

            
        }
        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            //Basically only connects to Valley or Another Slope. 
            List<Decision> possibleDecisions = new List<Decision>();
            foreach (Connection c in possibleMovements.Where(x => x.leadingTo is Slope || x.leadingTo is Valley))
            {
                if (!c.closed)
                {
                    possibleDecisions.Add(new Decision(c.leadingTo, 0));
                }
            }

            foreach (Decision possibleDecision in possibleDecisions)
            {
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
