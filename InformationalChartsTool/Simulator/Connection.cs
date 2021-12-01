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

        public static Connection GetIndexOfLocation(List<Connection> possibleMovements, Location location) //Find were a Location is in a list of Connections
        {
            foreach(Connection c in possibleMovements)
            {
                if(c.leadingTo == location)
                {
                    return c;
                }
            }
            throw new ArgumentException("Location not in list"); //Can we handle exception with out crash?
        }


    }
}
