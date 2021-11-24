using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SkidliftSys
{
    public class Person
    {
        public string name; //for esthetics?
        public int personNumber; //for keeping track of who is where.

        double morningness; //from 0-1? How early do they start skiing.
        double hungryness;  //from 0-1? how hungry they are.
        double skillLevel; //from 0-1? How good at skiing is the person.
        double explororness; //from 0-1? How much they want to visit new lifts.

        bool hungerState; //if true they want to find a restaurant
        bool doneSkiingState; //if true they want to go home

        public int timeLocation; //It is useful to know how long someone has been in a lift


        List<Decision> futureDecisions = new List<Decision>(); // do decisions require there own class?
        public List<Location> locationHistory = new List<Location>();

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

            switch (occupying) //Switch statement to determine what derived class occupying is.
            {
                case Lift _:
                    return LiftDecision(possibleMovements);
                case Slope _:
                    return SlopeDecision(possibleMovements);
                case LiftQueue _:
                    return LiftQueueDecision(possibleMovements);
                case Restaurant _:
                    return MountainTopDecision(possibleMovements);
                default:
                    throw new ArgumentException("Invalid Location");
            }
        }
        
        private Location LiftDecision(List<Connection> possibleMovements)
        {
            //Disregarding top of mountain location for now we can say that the person will look for the first slopes in the list and pick one.
            List<Slope> possibleSlopes = new List<Slope>();
            foreach(Connection i in possibleMovements)
            {
                if(i.leadingTo is Slope slope && !i.closed)
                {
                    possibleSlopes.Add(slope); 
                }
            }
            Random rnd = new Random();
            return(possibleSlopes[rnd.Next(0, possibleSlopes.Count)]); //Basic behaivour, pick a random slope.
        }

        private Location SlopeDecision(List<Connection> possibleMovements)
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
                if (i.leadingTo is LiftQueue && !i.closed)
                {
                    possibleQueues.Add(i);
                }
            }
            Random rnd = new Random();
            return possibleMovements[rnd.Next(0, possibleMovements.Count)].leadingTo;
                //possibleQueues[rnd.Next(0,possibleQueues.Count)].leadingTo; //Basic behaivour, pick a random queue
        }

        private Location LiftQueueDecision(List<Connection> possibleMovements) //note that this should in normal circumstances not be called
        {
            return (possibleMovements[1].leadingTo);
        }

        private Location MountainTopDecision(List<Connection> possibleMovements) //look for slopes or restaurants primarily.
        {
            return (possibleMovements[1].leadingTo);
        }
    }
}
