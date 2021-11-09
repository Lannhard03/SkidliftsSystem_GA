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
            for(int i = 0; i<200; i++)
            {
                alloccupants.Add(new Person(i));
            }
            Liftqueue superko = new Liftqueue(alloccupants, 2, 10, "superkö");
            Lift superliften = new Lift(200, "superliften");
            Slope superbacken = new Slope(500, "superbacken");

            //make connections
            superko.possiblemovements.Add(superliften);
            superliften.possiblemovements.Add(superbacken);
            superbacken.possiblemovements.Add(superko);


            while(time <= endtime)
            {
                superko.Liftpeople(timestep);
                superliften.MoveLift(timestep);
                superbacken.SlopeMovement(timestep);
                if(time%100 == 0)
                {
                    Console.WriteLine("Time: {0}", time);
                    Console.WriteLine("Superko har {0} köande", superko.occupants.Count);
                    Console.WriteLine("SuperLiften har {0} personer i sig", superliften.occupants.Count);
                    Console.WriteLine("Superbacken har {0} åkande", superbacken.occupants.Count);
                }
                

                time += timestep;
            }
            foreach(Location i in alloccupants[5].location_history)
            {
                Console.WriteLine(i.nameReference);
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
