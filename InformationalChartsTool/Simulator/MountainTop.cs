using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace InformationalChartsTool
{
    class MountainTop : Location
    {
        //Many Lifts may lead to this place, and people may spend some time waiting here.

        public override void Update(int timeStep)
        {
            foreach(Person i in occupants)
            {
                
                this.Decision(i, possibleMovements).MovePerson(i, this);
                //Class may seem unnessecary but it will govern what type of choices Persons make
            }
        }

        public override Location Decision(Person decisionMaker, List<Connection> possibleMovements)
        {
            int skillFactor = 100;
            int explororFactor = 100;
            double explororExponent = 1;
            double explororMultiple = 1;

            double skillnessSteppness = 1;



            List<Decision> weightedSlopeList = new List<Decision>();
            foreach (Connection i in possibleMovements)
            {
                if (!i.closed && i.leadingTo is Slope s)
                {
                    weightedSlopeList.Add(new Decision(s, 0));
                }
            }

            foreach (Decision i in weightedSlopeList)
            {
                //Factor in Skill, Explororness, Hungryness
                //this solution is terrible, but how else to do it?
                if (i.decision is Slope s)
                {
                    int occurences = decisionMaker.locationHistory.Where(x => x.Equals(i.decision)).Count(); //gets the amount of times Person has been at location

                    i.weight += 2 * explororFactor * (decisionMaker.explororness - 0.5) * Math.Exp(-occurences) +
                                Math.Exp(-occurences) * explororFactor * (1 - decisionMaker.explororness) +
                                (1 - Math.Exp(-occurences)) * (explororFactor / (1 + Math.Exp(explororMultiple * Math.Pow(2 * (decisionMaker.explororness - 0.5), explororExponent) * occurences)));

                    i.weight += Math.Exp(-skillnessSteppness * Math.Pow(s.difficulty - decisionMaker.skillLevel, 2));
                }
            }
            return possibleMovements[1].leadingTo;

        }
    }
}
