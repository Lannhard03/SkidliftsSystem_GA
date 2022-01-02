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
    }
}
