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
        static public int time = 0;
        static public List<Person> allOccupants = new List<Person>();
        static public List<Location> allLocations = new List<Location>();

        public static void RunSimulation()
        {
            //initialize
            #region
            int timeStep = 1; //one second?
            int endTime = 28800;

            

            for (int i = 0; i < 1000; i++)
            {
                allOccupants.Add(new Person(i, NameGenerator()));
            }
            List<Person> temp = new List<Person>();
            foreach (Person p in allOccupants)
            {
                temp.Add(p);
            }

            Home home1 = new Home("Stora dalen boende", temp);

            Restaurant restaurant1 = new Restaurant(200, "Stora dalen restaurang");

            Valley valley1 = new Valley("Stora dalen");
            Valley valley2 = new Valley("lilla dalen");

            MountainTop berg1 = new MountainTop("höga toppen");
            MountainTop berg2 = new MountainTop("korta toppen");

            LiftQueue ko1 = new LiftQueue(6, 8, "superkö");
            LiftQueue ko2 = new LiftQueue(4, 8, "springkö");
            LiftQueue ko3 = new LiftQueue(2, 7, "Kortkö");

            Lift lift1 = new Lift(200, "superliften");
            Lift lift2 = new Lift(500, "springliften");
            Lift lift3 = new Lift(150, "Kortaliften");

            Slope backe1 = new Slope(250, "superbacken", 0.2);
            Slope backe2 = new Slope(500, "springBacken", 0.4);
            Slope backe3 = new Slope(100, "kortabacken", 0.2);

            //make connections
            home1.possibleMovements.Add(new Connection(valley1));

            valley1.possibleMovements.Add(new Connection(ko1));
            valley1.possibleMovements.Add(new Connection(ko3));
            valley1.possibleMovements.Add(new Connection(restaurant1));

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

            restaurant1.possibleMovements.Add(new Connection(valley1));

            //add Locations to meta list
            allLocations.Add(restaurant1);
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
            #endregion

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


        }

        //Update every Location, add hunger/tiredness and open closed connections
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
                if(time % 10 == 0)
                {
                    l.timeBasedOccupantCounts.Add(l.occupants.Count);
                }
                
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
            foreach (Person p in allOccupants)
            {
                p.hunger += p.hungryness * 0.0001;
                p.tired += p.tiredness * 0.0001; //very terrible implementation
            }

            foreach (Location l in allLocations)
            {
                foreach (Connection c in l.possibleMovements)
                {
                    if (c.closed)
                    {
                        c.closed = false; //For now this is fine, since closure will be checked everytime anyway.
                        //Console.WriteLine("open closed location");
                    }
                }
            }

        }

        //Based on "Files" file
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