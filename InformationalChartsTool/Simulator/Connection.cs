using System;
using System.Collections.Generic;
using System.Text;

namespace InformationalChartsTool
{
    public class Connection
    {
        public Location leadingTo;
        public bool closed;

        public Connection(Location leadingTo)
        {
            this.leadingTo = leadingTo;
            closed = false;
        }

        public static int GetIndexOfLocation(List<Connection> possibleMovements, Location location) //Find were a Location is in a list of Connections
        {
            for(int i = 0; i<possibleMovements.Count; i++)
            {
                if(possibleMovements[i].leadingTo == location)
                {
                    return i;
                }
            }
            throw new ArgumentException("Location not in list"); //Can we handle exception with out crash?
        }


    }
}
