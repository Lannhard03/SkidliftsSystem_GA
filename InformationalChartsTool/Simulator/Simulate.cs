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
        public static void BeginSimulation(string[] args)

        {
            //initialize
            #region
            int time = 0; //time 0 is the start of the skiday
            int timeStep = 1; //one second?
            int endTime = 28800;

            List<Person> allOccupants = new List<Person>();
            List<Location> allLocations = new List<Location>();

            for (int i = 0; i < 1000; i++)
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

            LiftQueue superKo = new LiftQueue(superKoStart, 2, 10, "superkö");
            LiftQueue springKo = new LiftQueue(springKoStart, 6, 20, "springkö");

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

            //add Locations to meta list
            allLocations.Add(superKo);
            allLocations.Add(springKo);
            allLocations.Add(superLiften);
            allLocations.Add(springLiften);
            allLocations.Add(superBacken);
            allLocations.Add(springBacken);
            #endregion

            Console.Write(String.Format("{0, 10}{1, 40}{2, 50}{3, 60}\n\n", "Tid:", "Köande till {4}:", "Åkande i {4}:", "Åkande i {5}:")); //lägg till namn på lift och backe
            while (time <= endTime)
            {
                UpdateSystem(timeStep, allLocations);

                if (time%100 == 0)
                {
                    Console.WriteLine("{0, 10:N0} {1, 40:N0} {2, 50:N0} {3, 60:N0}\n",
                        time, springKo.occupants.Count, springLiften.occupants.Count, springBacken.occupants.Count);
                }
                time += timeStep;
            }
            Console.WriteLine(allOccupants[5].name);
            foreach(Location i in allOccupants[5].locationHistory)
            {
                Console.WriteLine(i.name);
            }
        }

        //add group/method containing all queues, make a "liftmaker"
        //Liftmaker needs to make the queue, lift and slope in one instance, to make sure they are connected
        //Maybe make a list of complete lifts? Alternatively always use the separate lists in a for loop with every lift slope and queue having the same index
        static object LiftMaker(int amount)
        {
            List<LiftQueue> queues = new List<LiftQueue>();
            List<Lift> lifts = new List<Lift>();
            List<Slope> slopes = new List<Slope>();

            return queues;

        }


        //Loops through all location types and calls their respective "move" functions
        static public void UpdateSystem(int timeStep, List<Location> allLocations)
        {
            //we would like to update ceratain types of places before others.
            //Restaurant (and places that are full last) movement last
            //uppdate connections: Lifts may open restaurants may no longer be full

            int debugCheck = allLocations.Count;
            int debugCounter = 0;

            List<string> allLocationTypes = new List<string>();
            foreach (Type type in Assembly.GetAssembly(typeof(Location)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Location))))
            {
                allLocationTypes.Add(type.Name);
            }
            foreach(String s in allLocationTypes)
            {
                Console.WriteLine("Checking location type: {0}", s);
                foreach(Location l in allLocations)
                {
                    string methodName = s + "Move";
                    if (l.GetType().Name == s)
                    {
                        l.GetType().GetMethod(methodName).Invoke(l, new object[] { timeStep });
                        debugCounter++;
                    }
                }
            }
            if (debugCounter != debugCheck)
            {
                Console.WriteLine("Amount of Locations didn't add up! UpdateSystem method");
            }


        }

        //generates a random name (first and last), based on the files in Files folder
        static public string NameGenerator() 
        {
            string[] firstNames = File.ReadAllLines("Files\\FirstNames.txt");
            string[] lastNames = File.ReadAllLines("Files\\LastNames.txt");

            Random rnd = new Random();

            string name = firstNames[rnd.Next(0, firstNames.Length - 1)] + lastNames[rnd.Next(0, lastNames.Length - 1)];
            return name;
        }
    }
}
