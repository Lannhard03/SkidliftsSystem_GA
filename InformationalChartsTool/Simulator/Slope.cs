using System;
using System.Collections.Generic;
using System.Linq;

namespace InformationalChartsTool
{
    class Slope : Location
    {
        
        int slopeTime;
        public int difficulty; //1,2,3,4 -> green, blue, red, black
        public Slope(List<Person> occupants, int slopeTime, string name)
        {
            this.name = name;
            this.occupants = occupants;
            this.slopeTime = slopeTime;
            this.difficulty = 2;
        }
        public Slope(int slopeTime, string name)
        {
            this.name = name;
            this.slopeTime = slopeTime;
            this.difficulty = 2;
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
                this.Decision(i, possibleMovements).MovePerson(i, this);
            }

            
        }

        public override Location Decision(Person decisionMaker, List<Connection> possibleMovements)
        {
            List<Connection> possibleSlopes = new List<Connection>();
            foreach (Connection i in possibleMovements)
            {
                if (i.leadingTo is Slope slope && !i.closed)
                {
                    possibleSlopes.Add(i);
                }
            }

            List<Connection> possibleQueues = new List<Connection>();
            foreach (Connection i in possibleMovements)
            {
                if (i.leadingTo is LiftQueue && !i.closed)
                {
                    possibleQueues.Add(i);
                }
            }
            Random rnd = new Random();
            if (possibleQueues.Count <= 0)
            {
                return (possibleSlopes[rnd.Next(0, possibleSlopes.Count)].leadingTo);
            }
            else
            {
                return possibleQueues[rnd.Next(0, possibleQueues.Count)].leadingTo; //Basic behaivour, pick a random queue
            }
        }


    }
}
