using System;
using System.Collections.Generic;
using System.Text;

namespace InformationalChartsTool
{
    public class Decision
    {
        public Location decision;
        public double weight;
        public Decision(Location decision, int weight)
        {
            this.decision = decision;
            this.weight = weight;
        }
    }
}
