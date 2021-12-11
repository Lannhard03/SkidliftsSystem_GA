using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Linq;

namespace InformationalChartsTool
{
    //4 different types of things are required atleast: People, Locations, Movement and Decisions.
    // Locations will govern possible movements (maybe with connections).
    // People will be in a location, and decide one of the possible movements based on either Decisions or Location (depending on were the are).
    // Decisions are governed by people but restricted by Location.

    public class Simulate
    {
        static public List<string> allLocationTypes = GenerateAllLocationTypes();

        static List<string> GenerateAllLocationTypes()
        {
            //Generates all derived classes of Location
            List<string> allLocationTypes = new List<string>();
            foreach (Type type in Assembly.GetAssembly(typeof(Location)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Location))))
            {
                allLocationTypes.Add(type.Name);
            }
            return allLocationTypes;

        } //It would be good if this list was sorted s.t. ex. Restaurant is called last.
        public static void BeginSimulation()

        {
            //initialized
            #region
            int time = 0; //time 0 is the start of the skiday
            int timeStep = 1; //one second?
            int endTime = 28800;

            List<Person> allOccupants = new List<Person>();
            List<Location> allLocations = new List<Location>();

            for (int i = 0; i < 250; i++)
            {
                allOccupants.Add(new Person(i, NameGenerator()));
            }

            List<Person> superKoStart = new List<Person>();
            for (int i = 0; i < 200; i++)
            {
                superKoStart.Add(allOccupants[i]);
            }

            List<Person> springKoStart = new List<Person>();
            for (int i = 200; i < allOccupants.Count; i++)
            {
                springKoStart.Add(allOccupants[i]);
            }

            Valley valley1 = new Valley("Stora dalen");
            Valley valley2 = new Valley("lilla dalen");

            MountainTop berg1 = new MountainTop("höga toppen");
            MountainTop berg2 = new MountainTop("korta toppen");

            LiftQueue ko1 = new LiftQueue(superKoStart, 6, 30, "superkö");
            LiftQueue ko2 = new LiftQueue(springKoStart, 4, 25, "springkö");
            LiftQueue ko3 = new LiftQueue(2, 10, "Kortkö");

            Lift lift1 = new Lift(200, "superliften");
            Lift lift2 = new Lift(500, "springliften");
            Lift lift3 = new Lift(50, "Kortaliften");

            Slope backe1 = new Slope(500, "superbacken");
            Slope backe2 = new Slope(250, "springBacken");
            Slope backe3 = new Slope(100, "kortabacken");

            //make connections
            valley1.possibleMovements.Add(new Connection(ko1));
            valley1.possibleMovements.Add(new Connection(ko3));

            valley2.possibleMovements.Add(new Connection(ko2));

            berg1.possibleMovements.Add(new Connection(backe1));
            berg1.possibleMovements.Add(new Connection(backe2));

            berg2.possibleMovements.Add(new Connection(backe3));

            ko1.possibleMovements.Add(new Connection(lift1));
            ko2.possibleMovements.Add(new Connection(lift2));
            ko3.possibleMovements.Add(new Connection(lift3));


            lift1.possibleMovements.Add(new Connection(berg1));
            lift2.possibleMovements.Add(new Connection(berg1));
            lift3.possibleMovements.Add(new Connection(berg2));

            backe1.possibleMovements.Add(new Connection(valley1));
            backe2.possibleMovements.Add(new Connection(valley2));
            backe3.possibleMovements.Add(new Connection(valley2));

            //add Locations to meta list
            allLocations.Add(valley1);
            allLocations.Add(valley2);
            allLocations.Add(berg1);
            allLocations.Add(berg2);
            allLocations.Add(ko1);
            allLocations.Add(ko2);
            allLocations.Add(ko3);
            allLocations.Add(lift1);
            allLocations.Add(lift2);
            allLocations.Add(lift3);
            allLocations.Add(backe1);
            allLocations.Add(backe2);
            allLocations.Add(backe3);
            #endregion

            Console.Write(String.Format("{0, 10}{1, 40}{2, 50}{3, 60}\n\n", "Tid:", "Köande till {0}:", "Åkande i {1}:", "Åkande i {2}:"), valley1.name, ko3.name, backe1.name); //lägg till namn på lift och backe
            while (time <= endTime)
            {
                UpdateSystem(timeStep, allLocations, allOccupants);

                if (time%100 == 0)
                {
                    Console.WriteLine("{0, 10:N0} {1, 40:N0} {2, 50:N0} {3, 60:N0}\n",
                        time, valley1.occupants.Count, ko3.occupants.Count, backe1.occupants.Count);
                }
                time += timeStep;
            }
            Console.WriteLine(allOccupants[5].name);
            foreach(Location i in allOccupants[5].locationHistory)
            {
                Console.WriteLine(i.name);
            }
            Console.WriteLine(allOccupants[5].explororness);
            foreach(Location l in allLocations)
            {
                Console.WriteLine("Location: {0} had {1} people in it", l.name, l.occupants.Count);
            }
        }
        static object LiftMaker(int amount)
        {
            //add group/method containing all queues, make a "liftmaker"
            //Liftmaker needs to make the queue, lift and slope in one instance, to make sure they are connected
            //Maybe make a list of complete lifts? Alternatively always use the separate lists in a for loop with every lift slope and queue having the same index
            List<LiftQueue> queues = new List<LiftQueue>();
            List<Lift> lifts = new List<Lift>();
            List<Slope> slopes = new List<Slope>();

            return queues;

        }
        static public void UpdateSystem(int timeStep, List<Location> allLocations, List<Person> allOccupants)
        {
            //int allOccupants = 0;
            //foreach (Location l in allLocations)
            //{
            //    allOccupants += l.occupants.Count;
            //}


            //Console.WriteLine("Before Update total amount: {0}", allOccupants);
            //allOccupants = 0;
            int debugCheck = allLocations.Count;
            int debugCounter = 0;
            foreach (Location l in allLocations)
            {
                l.Update(timeStep);
                //foreach (Location i in allLocations)
                //{
                //    allOccupants += i.occupants.Count;
                //}
                //Console.WriteLine("After Updating {0} total amount: {1}", l, allOccupants);
                //allOccupants = 0;
                debugCounter++;
            }
            if (debugCounter != debugCheck)
            {
                Console.WriteLine("Amount of Locations didn't add up! UpdateSystem method");
            }
            foreach(Person p in allOccupants)
            {
                p.hungryness += 0.00005;
            }

        }
        static public string NameGenerator() 
        {
            string[] firstNames = File.ReadAllLines("Files\\FirstNames.txt");
            string[] lastNames = File.ReadAllLines("Files\\LastNames.txt");

            Random rnd = new Random();

            string name = firstNames[rnd.Next(0, firstNames.Length - 1)] + lastNames[rnd.Next(0, lastNames.Length - 1)];
            //Concates a random first and lastname from files

            return name;
        }
    }
}
