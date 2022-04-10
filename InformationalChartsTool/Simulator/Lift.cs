using System;
using System.Collections.Generic;
using System.Linq;

namespace InformationalChartsTool
{
    class Lift : Location
    {
        int liftingTime;

        public Lift(int liftingTime, string name)
        {
            this.name = name;
            this.liftingTime = liftingTime;
        }
        public override void Update(int timeStep)
        {
            List<Person> movingPeople = new List<Person>(); //utilized since we want sorted output
            foreach (Person p in occupants)
            {
                p.timeLocation += timeStep; 
                if (p.timeLocation >= liftingTime)
                {
                    movingPeople.Add(p);
                }
            }

            movingPeople.Sort((x,y) => y.timeLocation.CompareTo(x.timeLocation)); //sorts output S.T person with most time exits first

            foreach (Person p in movingPeople)
            {
                MakeDecision(p, possibleMovements).MovePerson(p, this);
            }
        }
        
        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            //Look for MountainTop or Slopes and Lifts (we may jump off lift in middle)
            Connection temp = possibleMovements.Find(x => x.leadingTo is MountainTop && !x.closed);
            if (temp != null)
            {
                return temp.leadingTo;
            }
            
            temp = possibleMovements.Find(x => x.leadingTo is Lift && !x.closed);
            if (temp != null)
            {
                return temp.leadingTo;
            }
            //If there is a Lift go there.

            return possibleMovements[0].leadingTo;
        }
    }
}
