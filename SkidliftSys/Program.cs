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

            //add group/method containing all queues, make a "liftmaker"
            //Liftmaker needs to make the queue, lift and slope in one instance, to make sure they are connected
            //Maybe make a list of complete lifts? Alternatively always use the separate lists in a for loop with every lift slope and queue having the same index
            /* static object Liftmaker(int amount)
            {
                List<Liftqueue> queues = new List<Liftqueue>();
                List<Lift> lifts = new List<Lift>();
                List<Slope> slopes = new List<Slope>();

                return queues;

            }

            Liftqueue superko = new Liftqueue(alloccupants, 2, 10, "superkö");
            Lift superliften = new Lift(200, "superliften");
            Slope superbacken = new Slope(500, "superbacken");


            //make connections
            superko.possiblemovements.Add(superliften);
            superliften.possiblemovements.Add(superbacken);
            superbacken.possiblemovements.Add(superko);
            Console.Write(String.Format("{0, 10}{1, 40}{2, 50}{3, 60}\n\n", "Tid:", "Köande till {4}:", "Åkande i {4}:", "Åkande i {5}:")); //lägg till namn på lift och backe
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
