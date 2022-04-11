using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationalChartsTool
{
    class Home : Location
    {
        //for debugging
        public List<double> hungers = new List<double>();

        public Home(string name, List<Person> occupants)
        {
            this.name = name;
            this.occupants = occupants;
        }

        public override void Update(int timeStep)
        {
            int waitTimeMultiplier = 7200; //maximum waittime of location

            for (int i = 0; i < occupants.Count; i++)
            {
                occupants[i].timeLocation += timeStep;
                if (occupants[i].morningness * waitTimeMultiplier < occupants[i].timeLocation && occupants[i].tired < 0.8) //if tired enough people won't leave
                {
                    occupants[i].hunger = 0;
                    MakeDecision(occupants[i], possibleMovements).MovePerson(occupants[i], this);
                    i--;
                }
            }
        }

        //overridden for debugging purposes
        public override void MovePerson(Person person, Location comingFrom)
        {
            base.MovePerson(person, comingFrom);
            hungers.Add(person.hunger);
        }
    }
}
