﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SkidliftSys
{
    public class Person
    {
        string name; //for esthetics?
        public int personNumber; //for keeping track of who is where.

        int morningness; //from 0-1? How early do they start skiing.
        int hungryness;  //from 0-1? how hungry they are.
        int skillLevel; //from 0-1? How good at skiing is the person.
        int explororness; //from 0-1? How much they want to visit new lifts.

        bool hungerState; //if true they want to find a restaurant
        bool doneSkiingState; //if true they want to go home

        public int timeLocation; //It is useful to know how long someone has been in a lift


        List<Decision> futureDecisions = new List<Decision>(); // do decisions require there own class?
        public List<Location> locationHistory = new List<Location>();

        public Person(int personNumber)
        {
            this.personNumber = personNumber;
        }
        public Location DecisionHandler(List<Location> possibleMovements, Location occupying)
        {

            //Depending in which Location type Person is occupying we make different decisions.

            switch (occupying) //Switch statement to determine what derived class occupying is.
            {
                case Lift _:
                    return (LiftDecision(possibleMovements));
                    
                case Slope _:
                    return(SlopeDecision(possibleMovements));
                default:
                    throw new ArgumentException("Invalid Location");
            }
        }
        
        private Location LiftDecision(List<Location> possibleMovements)
        {
            //Disregarding top of mountain location for now we can say that the person will look for the first slopes in the list and pick one.
            List<Slope> possibleSlopes = new List<Slope>();
            foreach(Location i in possibleMovements)
            {
                if(i is Slope slope)
                {
                    possibleSlopes.Add(slope); 
                }
            }
            Random rnd = new Random();
            return(possibleSlopes[rnd.Next(0, possibleSlopes.Count - 1)]); //Basic behaivour, pick a random slope.
        }

        private Location SlopeDecision(List<Location> possibleMovements)
        {
            List<Liftqueue> possibleQueues = new List<Liftqueue>();
            foreach(Location i in possibleMovements)
            {
                if(i is Liftqueue queue)
                {
                    possibleQueues.Add(queue);
                }
            }
            Random rnd = new Random();
            return (possibleQueues[rnd.Next(0,possibleQueues.Count-1)]); //Basic behaivour, pick a random queue
        }
    }
}
