using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    //4 different types of things are required atleast: People, Locations, Movement and Decisions.
    // Locations will govern possible movements (maybe with connections).
    // People will be in a location, and decide one of the possible movements based on either Decisions or Location (depending on were the are).
    // Decisions are governed by people but restricted by Location.

    class Program
    {
        static void Main(string[] args)
        {
            int time = 0; //time 0 is the start of the skiday
            int timestep = 1; //one second?

            //initialize

            while (true) //We Loop through for an entire day?
            {
                //Slopemovement (people get to lifts)
                //Liftmovement (lifts lift people)
                //PeopleDecisions (people decide were to go)
                    // Pathfinding, ex. Person wants to get from point A to B, we need to calculate what decisions are required and store them for next decision?
                // Collect/store data
                time +=timestep;
            }
        }
    }
}
