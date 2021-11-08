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
            int endtime = 28800;

            //initialize
            List<Person> alloccupants = new List<Person>();
            for(int i = 0; i<1000; i++)
            {
                alloccupants.Add(new Person(i));
            }
            Liftqueue superko = new Liftqueue(alloccupants, 2, 10);
            Lift superliften = new Lift(200);
            Slope superbacken = new Slope(500);

            //make connections
            superko.possiblemovements.Add(superliften);
            superliften.possiblemovements.Add(superbacken);
            superbacken.possiblemovements.Add(superko);
            Console.Write(String.Format("{0, 10}{1, 40}{2, 50}{3, 60}\n\n", "Tid:", "Köande till superliften:", "Åkande i superliften:", "Åkande i superbacken:"));
            while (time <= endtime)
            {
                superko.Liftpeople(timestep);
                superliften.MoveLift(timestep);
                superbacken.SlopeMovement(timestep);
                if (time%100 == 0)
                {
                    Console.WriteLine("{0, 10:N0} {1, 40:N0} {2, 50:N0} {3, 60:N0}\n",
                        time, superko.occupants.Count, superliften.occupants.Count, superbacken.occupants.Count);
                }
                

                time += timestep;
            }

            



            
            /*
            while (time<=endtime) //We Loop through for an entire day?
            {
                //Slopemovement (people get to lifts)
                //Liftmovement (lifts lift people)
                //PeopleDecisions (people decide were to go)
                    // Pathfinding, ex. Person wants to get from point A to B, we need to calculate what decisions are required and store them for next decision?
                // Collect/store data
                time +=timestep;
            }
            */
        }

    }
}
