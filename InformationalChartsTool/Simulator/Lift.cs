using System;
using System.Collections.Generic;
using System.Linq;

namespace InformationalChartsTool
{
    class Lift : Location
    {
        //Time Person spends in lift
        int liftingTime;

        public Lift(int liftingTime, string name)
        {
            this.name = name;
            this.liftingTime = liftingTime;
        }

        public override void Update(int timeStep)
        {
            List<Person> movingPeople = new List<Person>(); //used since we want sorted output
            foreach (Person p in occupants)
            {
                p.timeLocation += timeStep;
                if (p.timeLocation >= liftingTime)
                {
                    movingPeople.Add(p);
                }
            }

            movingPeople.Sort((x, y) => y.timeLocation.CompareTo(x.timeLocation)); //sorts output such that person with most time exits first

            foreach (Person p in movingPeople)
            {
                MakeDecision(p, possibleMovements).MovePerson(p, this);
            }
        }

        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            //Look for MountainTop, Slopes, or Lifts (we may jump off lift in middle), Note multiple return statements.

            //First MountainTops are always choosen
            Connection temp = possibleMovements.Find(x => x.leadingTo is MountainTop && !x.closed);
            if (temp != null)
            {
                return temp.leadingTo;
            }

            //If there is a Lift go there
            temp = possibleMovements.Find(x => x.leadingTo is Lift && !x.closed);
            if (temp != null)
            {
                return temp.leadingTo;
            }

            return possibleMovements[0].leadingTo;
        }
    }
}
