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

    static public class Simulate
    {
        static public int time = 0; //starting from 9:00
        static public List<Person> allOccupants = new List<Person>();
        static public List<Location> allLocations = new List<Location>();

        public static void RunSimulation()
        {
            //initialize
            int timeStep = 1; //one second
            int endTime = 32400; //from 9:00 to 18:00
            (allLocations, allOccupants) = SmallSystem();
            

            //update system every timestep
            while (time <= endTime)
            {
                UpdateSystem(timeStep, allLocations, allOccupants);

                time += timeStep;
            }

            //Print location history of person 5
            Console.WriteLine(allOccupants[5].name);
            foreach (Tuple<Location, int> i in allOccupants[5].locationHistory)
            {
                Console.WriteLine("{0,25:N0} {1, 20:N0}", i.Item1.name, i.Item2);
            }
            Console.Write("\n");

            //Print value of all doubles (attributes) of person 5
            foreach(FieldInfo info in typeof(Person).GetFields())
            {
                if(info.GetValue(allOccupants[5]) is double d)
                {
                    Console.WriteLine("{0} is: {1}", info.Name, d);
                }
            }

            //Occupant count of all locations at end of day
            foreach (Location l in allLocations)
            {
                Console.WriteLine("Location: {0} had {1} people in it", l.name, l.occupants.Count);
            }
            Console.WriteLine("\n");
            foreach (Location l in allLocations)
            {
                l.timeBasedOccupantCounts = ListCompressor(l.timeBasedOccupantCounts);
            }

        }

        //Update every Location, add hunger/tiredness and open closed connections
        static void UpdateSystem(int timeStep, List<Location> allLocations, List<Person> allOccupants)
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
                l.timeBasedOccupantCounts.Add(l.occupants.Count);
                l.Update(timeStep);
                debugCounter++;
            }
            if (debugCounter != debugCheck)
            {
                Console.WriteLine("Amount of Locations didn't add up! UpdateSystem method");
            }
            foreach (Person p in allOccupants)
            {
                p.hunger += 5 * Math.Pow(10, -5) + p.hungryness* 3.33 * Math.Pow(10, -5); //this will result in hunger of 0.9 at between 12:00 and 14:00
                p.tired += 2.77* Math.Pow(10, -5) + p.tiredness*1.388* Math.Pow(10, -5); //between 15:00 and 18:00 for 0.9
            }

            foreach (Location l in allLocations)
            {
                foreach (Connection c in l.possibleMovements)
                {
                    if (c.closed && c.leadingTo is Restaurant)
                    {
                        c.closed = false; //For now this is fine, since closure will be checked everytime anyway.
                        //Console.WriteLine("open closed location");
                    }
                }
            }

        }

        //Based on "Files" file
        static string NameGenerator()
        {
            string[] firstNames = File.ReadAllLines("Files\\FirstNames.txt");
            string[] lastNames = File.ReadAllLines("Files\\LastNames.txt");

            Random rnd = new Random();

            string name = firstNames[rnd.Next(0, firstNames.Length - 1)] + lastNames[rnd.Next(0, lastNames.Length - 1)];
            //Concates a random first and lastname from files

            return name;
        }
        
        static public List<int> ListCompressor(List<int> uncompressedData)
        {
            int regionLenght = 200; //must be a multiple of uncompressedData.Count
            int counter = 1;
            int value = 0;
            List<int> compressedData = new List<int>();
            for (int i = 0; i < uncompressedData.Count; i++)
            {
                if (i + 1 <= regionLenght*counter)
                {
                    value += uncompressedData[i];
                    //Console.WriteLine(value);
                }
                else
                {
                    compressedData.Add(value / regionLenght);
                    value = 0;
                    counter++;
                }
            }
            return compressedData;
        }

        static (List<Location>,List<Person>) SmallSystem()
        {
            List<Person> allOccupants = new List<Person>();
            List<Location> allLocations = new List<Location>();

            for (int i = 0; i < 700; i++)
            {
                allOccupants.Add(new Person(i, NameGenerator()));
            }
            List<Person> temp = new List<Person>();
            foreach (Person p in allOccupants)
            {
                temp.Add(p);
            }

            Home home1 = new Home("Stora dalen boende", temp);

            Restaurant restaurant1 = new Restaurant(150, "Stora dalen restaurang");
            Restaurant restaurant2 = new Restaurant(50, "Höga toppen restaurang");

            Valley valley1 = new Valley("Stora dalen");
            Valley valley2 = new Valley("lilla dalen");

            MountainTop berg1 = new MountainTop("höga toppen");
            MountainTop berg2 = new MountainTop("korta toppen");

            LiftQueue ko1 = new LiftQueue(6, 8, "superkö");
            LiftQueue ko2 = new LiftQueue(4, 8, "springkö");
            LiftQueue ko3 = new LiftQueue(2, 7, "Kortkö");

            Lift lift1 = new Lift(240, "superliften");
            Lift lift2 = new Lift(500, "springliften");
            Lift lift3 = new Lift(150, "Kortaliften");

            Slope backe1 = new Slope(100, "superbacken", 0.4);
            Slope backe2 = new Slope(150, "springBacken", 0.4);
            Slope backe3 = new Slope(50, "kortabacken", 0.2);

            //make connections
            home1.possibleMovements.Add(new Connection(valley1));

            valley1.possibleMovements.Add(new Connection(ko1));
            valley1.possibleMovements.Add(new Connection(ko3));
            valley1.possibleMovements.Add(new Connection(restaurant1));
            valley1.possibleMovements.Add(new Connection(home1));

            valley2.possibleMovements.Add(new Connection(ko2));

            berg1.possibleMovements.Add(new Connection(backe1));
            berg1.possibleMovements.Add(new Connection(backe2));
            berg1.possibleMovements.Add(new Connection(restaurant2));

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

            restaurant1.possibleMovements.Add(new Connection(valley1));

            restaurant2.possibleMovements.Add(new Connection(berg1));

            //add Locations to meta list
            allLocations.Add(restaurant1);
            allLocations.Add(restaurant2);
            allLocations.Add(home1);
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

            return (allLocations, allOccupants);
        }
    }
}