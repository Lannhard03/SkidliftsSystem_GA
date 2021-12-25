﻿using System;
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
            int waittimeMultiplier = 7200;

            for (int i = 0; i < occupants.Count; i++)
            {
                occupants[i].timeLocation += timeStep;
                if (occupants[i].morningness * waittimeMultiplier < occupants[i].timeLocation)
                {
                    MakeDecision(occupants[i], possibleMovements).MovePerson(occupants[i], this);
                }
            }
        }


        public override Location MakeDecision(Person decisionMaker, List<Connection> possibleMovements)
        {
            //example functions
            foreach(Connection c in possibleMovements)
            {
                return c.leadingTo;
            }
            return possibleMovements[0].leadingTo;
        }
    }
}