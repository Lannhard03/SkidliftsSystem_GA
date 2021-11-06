using System;
using System.Collections.Generic;

namespace SkidliftSys
{
    public class Person
    {
        string name; //for estetic?
        public int person_number; //for keeping track of who is where.

        int morningness; //from 0-1? How early do they start skiing.
        int hungryness;  //from 0-1? how hungry they are.
        int skill_level; //from 0-1? How good at skiing is the person.
        int explororness; //from 0-1? How much they want to visit new lifts.

        bool hunger_state; //if true they want to find a restaurant
        bool done_skiing_state; //if true they want to go home

        public int time_location; //It is useful to know how long someone has been in a lift


        List<Decision> future_decisions = new List<Decision>(); // do decisions require there own class?
        List<Location> location_history = new List<Location>();

        public Person(int number)
        {
            person_number = number;
        }
        public Location DecisionHandler(List<Location> possiblemovements, Location occupying)
        {

            //Depending in which Location type Person is occupying we make different decisions.

            switch (occupying) //Switch statement to determine what derived class occupying is.
            {
                case Lift l:
                    return(LiftDecision(possiblemovements));
                    
                case Slope l:
                    return(SlopeDecision(possiblemovements));
                default:
                    throw new ArgumentException("Invalid Location");
            }
        }
        
        private Location LiftDecision(List<Location> possiblemovements)
        {
            //Disregarding top of mountain location for now we can say that the person will look for the first slopes in the list and pick one.
            List<Slope> possibleslopes = new List<Slope>();
            foreach(Location i in possiblemovements)
            {
                if(i is Slope slope)
                {
                    possibleslopes.Add(slope); //This may cause a problem (i should be Slope but maybe i still "converts" it?
                }
            }
            Random rnd = new Random();
            return(possibleslopes[rnd.Next(0, possibleslopes.Count - 1)]); //Basic behaivour, pick a random slope.
        }

        private Location SlopeDecision(List<Location> possiblemovements)
        {
            List<Liftqueue> possiblequeues = new List<Liftqueue>();
            foreach(Location i in possiblemovements)
            {
                if(i is Liftqueue queue)
                {
                    possiblequeues.Add(queue);
                }
            }
            Random rnd = new Random();
            return (possiblequeues[rnd.Next(0,possiblequeues.Count-1)]); //Basic behaivour, pick a random queue
        }


    }
}
