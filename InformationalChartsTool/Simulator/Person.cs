using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;

namespace InformationalChartsTool
{
    public class Person
    {
        static Random rnd = new Random();

        public string name;
        int personNumber; //for keeping track of who is where.

        //range from 0-1, see documentation for purpose
        public double morningness;
        public double hungriness;
        public double queuePatience;
        public double chill;
        public double tiredness;
        public double skillLevel;
        public double curiousness;

        //increase overtime
        public double hunger;
        public double tired;

        public int timeLocation;

        //for graphing
        public List<Tuple<Location, int>> locationHistory = new List<Tuple<Location, int>>();

        public Person(int personNumber)
        {
            this.personNumber = personNumber;
            tiredness = rnd.NextDouble();
            skillLevel = rnd.NextDouble();
            morningness = rnd.NextDouble();
            hungriness = rnd.NextDouble();
            curiousness = rnd.NextDouble();
            queuePatience = rnd.NextDouble();
            chill = rnd.NextDouble();
        }

        public Person(int personNumber, string name)
        {
            this.personNumber = personNumber;
            tiredness = rnd.NextDouble();
            skillLevel = rnd.NextDouble();
            morningness = rnd.NextDouble();
            hungriness = rnd.NextDouble();
            curiousness = rnd.NextDouble();
            queuePatience = rnd.NextDouble();
            chill = rnd.NextDouble();
            this.name = name;
        }

        //see documentation for more detail on weightfunction workings
        public double WeightCuriousness(int curiouWeight, Decision checkingDecision)
        {
            double explororExponent = 3;
            double explororMultiple = 1;

            int occurences = locationHistory.Where(x => x.Item1.Equals(checkingDecision.decision)).Count(); //gets the amount of times Person has been at location

            double temp = (2 * curiouWeight * (curiousness - 0.5) * Math.Exp(-occurences) +
                        Math.Exp(-occurences) * curiouWeight * (1 - curiousness) +
                        (1 - Math.Exp(-occurences)) * (curiouWeight / (1 + Math.Exp(explororMultiple * Math.Pow(2 * (curiousness - 0.5), explororExponent) * occurences))));
            return temp + (rnd.NextDouble() - 0.5) * 0.01;

        }

        public double WeightSkillLevel(int skillLevelWeight, Decision checkingDecision)
        {
            int skillFlatnessAtMax = 2; //2n
            double skillSteepness = 3; //R

            if (checkingDecision.decision is Slope slopeDecision)
            {
                double temp = skillLevelWeight * Math.Exp(-(skillSteepness / (Math.Pow(skillLevel, 2) + 0.1)) * (Math.Pow(Math.Log(skillLevel - slopeDecision.difficulty + 1), skillFlatnessAtMax)));
                return temp + (rnd.NextDouble() - 0.5) * 0.01;
            }
            else
            {
                Console.WriteLine("Tried getting difficulty of none Slope Location");
                return 0;
            }
        }

        public double WeightHunger(int hungerWeight, Decision checkingDecision)
        {
            double threshhold = 0.95;
            if (checkingDecision.decision is Restaurant)
            {
                return hungerWeight * (1 / (1 + Math.Exp(-100 * (hunger - threshhold))));
            }
            else
            {
                return 0;
            }
        }

        public double WeightTiredness(int tirednessWeight, Decision checkingDecision)
        {
            double threshhold = 0.9;
            if (checkingDecision.decision is Home)
            {
                return tirednessWeight * (1 / (1 + Math.Exp(-100 * (tired - threshhold))));
            }
            else
            {
                return 0;
            }
        }

        public double WeightQueueLength(int queueLengthWeight, Decision checkingDecision, int liftOccupants)
        {
            double tendencyTowardsEdges = -5; //large value towards 1, small linear, negative towards 0
            if (checkingDecision.decision is LiftQueue)
            {
                double length = (double)checkingDecision.decision.occupants.Count / (liftOccupants + 1);

                return queueLengthWeight * ((-1 / (1 - Math.Exp(-queuePatience * tendencyTowardsEdges)) * (Math.Exp(queuePatience * tendencyTowardsEdges * (length - 1)) - Math.Exp(-queuePatience * tendencyTowardsEdges))) + 1) + (rnd.NextDouble() - 0.5) * 0.01;
            }
            else
            {
                Console.WriteLine("Tried getting QueueLength of none LiftQueue");
                return 0;
            }
        }
    }
}
