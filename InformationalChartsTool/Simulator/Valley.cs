using System;
using System.Collections.Generic;
using System.Text;

namespace InformationalChartsTool
{
    class Valley : Location
    {
        public override void Update(int timstep)
        {
            
        }

        public override Location Decision(Person decisionMaker, List<Connection> possibleMovements)
        {
            return possibleMovements[0].leadingTo;
        }

    }
}
