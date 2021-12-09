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
            this.hungryness = rnd.NextDouble();
            this.explororness = rnd.NextDouble();
        }

        public Person(int personNumber, string name)
        {
            Random rnd = new Random();
            this.personNumber = personNumber;
            this.morningness = rnd.NextDouble();
            this.hungryness = rnd.NextDouble();
            this.explororness = rnd.NextDouble();
            this.name = name;
        }

        public Location DecisionHandler(List<Connection> possibleMovements, Location occupying)
        {
            //Depending in which Location type Person is occupying we make different decisions.
            foreach(string s in Simulate.allLocationTypes)
            {
                if(occupying.GetType().Name == s)
                {
                    MethodInfo methodName = GetType().GetMethod(s + "Decision");
                    Location output = (Location)methodName.Invoke(this, new object[] { possibleMovements });
                    return output;
                }
            }
            //Checks the type of Location called from to call the "...Decision" method of that derived class
            return null;
        }
        public Location LiftDecision(List<Connection> possibleMovements) //only call through handeler
        {
            //Disregarding top of mountain location for now we can say that the person will look for the first slopes in the list and pick one.
            foreach(Connection i in possibleMovements)
            {
                if(i.leadingTo is MountainTop && !i.closed)
                {
                    return i.leadingTo;
                }
            }

            List<Slope> possibleSlopes = new List<Slope>();
            foreach(Connection i in possibleMovements)
            {
                if(i.leadingTo is Slope slope && !i.closed)
                {
                    possibleSlopes.Add(slope); 
                }
            }
            Random rnd = new Random();
            return(possibleSlopes[0]); //Basic behaivour, pick a random slope.
        }
        public Location SlopeDecision(List<Connection> possibleMovements)
        {
            List<Connection> possibleSlopes = new List<Connection>();
            foreach (Connection i in possibleMovements)
            {
                if(i.leadingTo is Slope slope && !i.closed)
                {
                    possibleSlopes.Add(i);
                }
            }

            List<Connection> possibleQueues = new List<Connection>();
            foreach(Connection i in possibleMovements)
            {
                if(i.leadingTo is LiftQueue && !i.closed)
                {
                    possibleQueues.Add(i);
                }
            }
            Random rnd = new Random();
            if (possibleQueues.Count <= 0)
            {
                return (possibleSlopes[rnd.Next(0,possibleSlopes.Count)].leadingTo);
            }
            else
            {
                return possibleQueues[rnd.Next(0, possibleQueues.Count)].leadingTo; //Basic behaivour, pick a random queue
            }
            
        }
        public Location LiftQueueDecision(List<Connection> possibleMovements) //note that this should in normal circumstances not be called
        {
            return (possibleMovements[1].leadingTo);
        }
        public Location MountainTopDecision(List<Connection> possibleMovements) //look for slopes or restaurants primarily.
        {
            int skillFactor = 100;
            int explororFactor = 100;
            double explororExponent = 1;
            double explororMultiple = 1;




            int skillFlatnessAtMax = 2; //2n
            double skillSteepness = 3; //R



            List<Decision> weightedSlopeList = new List<Decision>();
            foreach(Connection i in possibleMovements)
            {
                if(!i.closed && i.leadingTo is Slope s)
                {
                    weightedSlopeList.Add(new Decision(s,0));
                }
            }

            foreach(Decision i in weightedSlopeList)
            {
                //Factor in Skill, Explororness, Hungryness
                //this solution is terrible, but how else to do it?
                if(i.decision is Slope s)
                {
                    int occurences = locationHistory.Where(x => x.Equals(i.decision)).Count(); //gets the amount of times Person has been at location

                    i.weight += 2 * explororFactor * (explororness - 0.5) * Math.Exp(-occurences) +
                                Math.Exp(-occurences) * explororFactor * (1 - explororness) +
                                (1 - Math.Exp(-occurences)) * (explororFactor / (1 + Math.Exp(explororMultiple * Math.Pow(2 * (explororness - 0.5), explororExponent) * occurences)));

                    i.weight += Math.Exp(
                                -(skillSteepness/(Math.Pow(skillLevel, 2)+0.1))*
                                (Math.Pow(Math.Log(skillLevel-s.difficulty+1),2)));


                }
            }
            return possibleMovements[1].leadingTo;

        }
    }
}
