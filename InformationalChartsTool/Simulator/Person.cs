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
        public string name; //for esthetics?
        public int personNumber; //for keeping track of who is where.

        public double morningness; //from 0-1? How early do they start skiing.
        public double hungryness;  //from 0-1? how hungry they are.
        public double queuePatients;
        public double chill;

        public double skillLevel; //from 0-1? How good at skiing is the person.
        public double explororness; //from 0-1? How much they want to visit new lifts.

        public bool hungerState; //if true they want to find a restaurant
        public bool doneSkiingState; //if true they want to go home

        public int timeLocation; 

        List<Decision> futureDecisions = new List<Decision>();
        public List<Location> locationHistory = new List<Location>();

        public Person(int personNumber)
        {
            Random rnd = new Random();
            this.personNumber = personNumber;
            this.morningness = rnd.NextDouble();
            this.hungryness = 0;
            this.explororness = rnd.NextDouble();
            this.queuePatients = rnd.NextDouble();
            this.chill = rnd.NextDouble();
        }

        public Person(int personNumber, string name)
        {
            Random rnd = new Random();
            this.personNumber = personNumber;
            this.morningness = rnd.NextDouble();
            this.hungryness = 0;
            this.explororness = rnd.NextDouble();
            this.queuePatients = rnd.NextDouble();
            this.chill = rnd.NextDouble();
            this.name = name;
        }
        public double WeightExplororness(int explororWeight, Decision checkingDecision)
        {
            double explororExponent = 1;
            double explororMultiple = 1;

            int occurences = locationHistory.Where(x => x.Equals(checkingDecision.decision)).Count(); //gets the amount of times Person has been at location

            double temp = (2 * explororWeight * (explororness - 0.5) * Math.Exp(-occurences) +
                        Math.Exp(-occurences) * explororWeight * (1 - explororness) +
                        (1 - Math.Exp(-occurences)) * (explororWeight / (1 + Math.Exp(explororMultiple * Math.Pow(2 * (explororness - 0.5), explororExponent) * occurences))));
            return temp;
            //Math function to get weight
        }
        public double WeightSkillLevel(int skillLevelWeight, Decision checkingDecision)
        {
            int skillFlatnessAtMax = 2; //2n
            double skillSteepness = 3; //R

            if (checkingDecision.decision is Slope slopeDecision)
            {
                double temp = skillLevelWeight * Math.Exp(-(skillSteepness / (Math.Pow(skillLevel, 2) + 0.1)) * (Math.Pow(Math.Log(skillLevel - slopeDecision.difficulty + 1), skillFlatnessAtMax)));
                return temp;
            }
            else
            {
                Console.WriteLine("Tried getting difficulty of none Slope Location");
                return 0;
            }


            
        }
        public double WeightHunger(int hungerWeight, Decision checkingDecision)
        {
            double tendancyTowardsEdges = 5; //large value towards 1, small linear, negative towards 0
            if(checkingDecision.decision is Restaurant)
            {
                //Assuming function on x [0,1], y [0,1] and exponential form
                return hungerWeight*(1 / (1 - Math.Exp(-tendancyTowardsEdges))) * (Math.Exp(tendancyTowardsEdges * (hungryness - 1)) + Math.Exp(-tendancyTowardsEdges));
            }
            else
            {
                return 0;
            }
        }
        public double WeightTiredness(int tirednessWeight, Decision checkingDecision)
        {
            double tendancyTowardsEdges = 5; //large value towards 1, small linear, negative towards 0
            if (checkingDecision.decision is Restaurant)
            {
                //Assuming function on x [0,1], y [0,1] and exponential form
                return tirednessWeight*(1 / (1 - Math.Exp(-tendancyTowardsEdges))) * (Math.Exp(tendancyTowardsEdges * (hungryness - 1)) + Math.Exp(-tendancyTowardsEdges));
                
            }
            else
            {
                return 0;
            }
        }
        public double WeightQueueLenght(int queueLenghtWeight, Decision checkingDecision)
        {
            double tendancyTowardsEdges = -5; //large value towards 1, small linear, negative towards 0
            if (checkingDecision.decision is LiftQueue)
            {
                int length = checkingDecision.decision.occupants.Count/10; //what to normalize with??
                return queueLenghtWeight * (1 - (1 / (1 - Math.Exp(-tendancyTowardsEdges)) * (Math.Exp(tendancyTowardsEdges * (length - 1)) - Math.Exp(-tendancyTowardsEdges))));
            }
            else
            {
                Console.WriteLine("Tried getting QueueLength of none LiftQueue");
                return 0;
            }
        }
    }
}
