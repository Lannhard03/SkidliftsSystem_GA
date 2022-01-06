using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationalChartsTool
{
    class Home : Location
    {
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
                }
            }
        }

        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            foreach(Connection c in possibleMovements)
            {
                return c.leadingTo;
            }
            return possibleMovements[0].leadingTo;
        }
    }
}
