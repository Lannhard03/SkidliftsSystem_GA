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
            int timeStep = 1; //one second?
            int endTime = 28800;
            
            //initialize
            List<Person> allOccupants = new List<Person>();
            for(int i = 0; i<1000; i++)
            {
                allOccupants.Add(new Person(i));
            }

            List<Person> superKoStart = new List<Person>();
            for(int i = 0; i<100; i++)
            {
                superKoStart.Add(allOccupants[i]);
            }

            List<Person> springKoStart = new List<Person>();
            for (int i = 100; i < allOccupants.Count; i++)
            {
                springKoStart.Add(allOccupants[i]);
            }

            LiftQueue superKo = new LiftQueue(superKoStart, 2, 10, "superkö");
            LiftQueue springKo = new LiftQueue(springKoStart,6, 20, "springkö");

            Lift superLiften = new Lift(200, "superliften");
            Lift springLiften = new Lift(500, "springliften");

            Slope superBacken = new Slope(500, "superbacken");
            Slope springBacken = new Slope(100, "springBacken");


            //make connections
            superKo.possibleMovements.Add(new Connection(superLiften));
            superLiften.possibleMovements.Add(new Connection(superBacken));
            superBacken.possibleMovements.Add(new Connection(superKo));
            superBacken.possibleMovements.Add(new Connection(springKo));

            springKo.possibleMovements.Add(new Connection(springLiften));
            springLiften.possibleMovements.Add(new Connection(springBacken));
            springBacken.possibleMovements.Add(new Connection(superBacken));




            Console.Write(String.Format("{0, 10}{1, 40}{2, 50}{3, 60}\n\n", "Tid:", "Köande till {4}:", "Åkande i {4}:", "Åkande i {5}:")); //lägg till namn på lift och backe
            while (time <= endTime)
            {
                superKo.Liftpeople(timeStep);
                springKo.Liftpeople(timeStep);

                superLiften.MoveLift(timeStep);
                springLiften.MoveLift(timeStep);

                superBacken.SlopeMovement(timeStep);
                springBacken.SlopeMovement(timeStep);

                if (time%100 == 0)
                {
                    Console.WriteLine("{0, 10:N0} {1, 40:N0} {2, 50:N0} {3, 60:N0}\n",
                        time, springKo.occupants.Count, springLiften.occupants.Count, springBacken.occupants.Count);
                }
                time += timeStep;
            }
            foreach(Location i in allOccupants[5].locationHistory)
            {
                Console.WriteLine(i.name);
            }

        }

        //add group/method containing all queues, make a "liftmaker"
        //Liftmaker needs to make the queue, lift and slope in one instance, to make sure they are connected
        //Maybe make a list of complete lifts? Alternatively always use the separate lists in a for loop with every lift slope and queue having the same index
        static object Liftmaker(int amount)
        {
            List<LiftQueue> queues = new List<LiftQueue>();
            List<Lift> lifts = new List<Lift>();
            List<Slope> slopes = new List<Slope>();

            return queues;

        }

    }
}
