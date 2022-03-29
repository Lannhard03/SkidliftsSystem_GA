using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Linq;

namespace InformationalChartsTool
{
    static public class Simulation
    {
        static public int time = 0; //starting from 9:00
        static public List<Person> allOccupants = new List<Person>();
        static public List<Location> allLocations = new List<Location>();

        public static void RunSimulation()
        {
            
            Console.OutputEncoding = Encoding.UTF8;
            //initialize
            Console.WriteLine("Initializing");
            int timeStep = 1; //one second
            int endTime = 32400; //from 9:00 to 18:00
            
            (allLocations, allOccupants) = BigSystem(); //SmallSystem();

            Console.Clear();
            Console.WriteLine("Starting Simulation");
            string[] progressBar = File.ReadAllLines("Files\\ProgressBar.txt");
            Console.WriteLine(progressBar[0]);
            int procentDone = 0;
            Stopwatch sp = new Stopwatch();
            sp.Start();
            
            //update system every timestep
            while (time <= endTime)
            {
                if(10 * time  >= procentDone*endTime) //create cool progressbar
                {
                    Console.Clear();
                    Console.WriteLine(progressBar[procentDone]);
                    procentDone++;
                }
                
                UpdateSystem(timeStep, allLocations, allOccupants);
                
                time += timeStep;
            }
            
            //compress location history
            foreach (Location l in allLocations)
            {
                l.timeBasedOccupantCounts = ListCompressor(l.timeBasedOccupantCounts);
            }
            sp.Stop();
            
            Console.WriteLine("Execution took {0} seconds",sp.Elapsed.ToString());
            Console.WriteLine("Opening LiveCharts");
        }

        //Update every Location, add hunger/tiredness and open closed connections
        static void UpdateSystem(
            int timeStep,
            List<Location> allLocations, 
            List<Person> allOccupants)
        {
            //update each location
            foreach (Location l in allLocations)
            {
                l.timeBasedOccupantCounts.Add(l.occupants.Count);
                l.Update(timeStep);
            }

            //Hunger and tiredness
            foreach (Person p in allOccupants)
            {
                p.hunger += timeStep*(7.937 * Math.Pow(10, -5)
                         + p.hungryness* 1.3228 * Math.Pow(10, -5));
                //this will result in hunger of 1 at between 12:00 and 12:30 (7,937, 0,3228)

                p.tired += timeStep*(3.086* Math.Pow(10, -5)
                        + p.tiredness*0.881* Math.Pow(10, -5));
                //between 16:00 and 18:00 for 1 (3.086, 0.881)
            }

            //open closed restaurants
            foreach (Location l in allLocations)
            {
                foreach (Connection c in l.possibleMovements)
                {
                    if (c.closed && c.leadingTo is Restaurant)
                    {
                        c.closed = false;
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
        
        //takes a list and make a shorter "averaged" one
        static public List<int> ListCompressor(List<int> uncompressedData)
        {
            int regionLenght = 32400/162; //must be a divisor of uncompressedData.Count
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

        //initialize a ski system
        static (List<Location>,List<Person>) SmallSystem()
        {
            List<Person> allOccupants = new List<Person>();
            List<Location> allLocations = new List<Location>();

            for (int i = 0; i < 500; i++)
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

            Lift lift1 = new Lift(300, "superliften");
            Lift lift2 = new Lift(500, "springliften");
            Lift lift3 = new Lift(100, "Kortaliften");

            Slope backe1 = new Slope(75, "superbacken", 0.4);
            Slope backe2 = new Slope(125, "springBacken", 0.4);
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

        static (List<Location>, List<Person>) BigSystem()
        {
            List<Person> allOccupants = new List<Person>();
            List<Location> allLocations = new List<Location>();


            for (int i = 0; i < 3700; i++)
            {
                allOccupants.Add(new Person(i, NameGenerator()));
            }
            List<Person> home1Start = new List<Person>();
            for(int i = 0; i<3000; i++)
            {
                home1Start.Add(allOccupants[i]);
            }
            List<Person> home2Start = new List<Person>();
            for (int i = home1Start.Count; i < allOccupants.Count; i++)
            {
                home2Start.Add(allOccupants[i]);
            }

            Home home1 = new Home("Stora hemmet", home1Start);
            Home home2 = new Home("Lilla hemmet", home2Start);

            Restaurant restaurant1 = new Restaurant(15000, "Mellan Restaurangen");
            Restaurant restaurant2 = new Restaurant(25000, "Top Restaurangen");
            Restaurant restaurant3 = new Restaurant(30000, "Dal Restaurangen");
            Restaurant restaurant4 = new Restaurant(50, "Fika plats 1");
            Restaurant restaurant5 = new Restaurant(50, "Fika plats 2");
            Restaurant restaurant6 = new Restaurant(50, "Fika plats 3");
            Restaurant restaurant7 = new Restaurant(50, "Fika plats 4");


            Valley valley1 = new Valley("Stora dalen");
            Valley valley2 = new Valley("Mitten dalen");
            Valley valley3 = new Valley("Bortre dalen");

            MountainTop berg1 = new MountainTop("Mellan toppen");
            MountainTop berg2 = new MountainTop("Höga toppen");
            MountainTop berg3 = new MountainTop("Bortre toppen");
            MountainTop berg4 = new MountainTop("Transport toppen");

            LiftQueue ko1 = new LiftQueue(8, 8, "Stora Kö");
            LiftQueue ko2 = new LiftQueue(2, 8, "Mellan kö");
            LiftQueue ko3 = new LiftQueue(8, 8, "Spring kö");
            LiftQueue ko35 = new LiftQueue(2, 8, "Sido kö");
            LiftQueue ko4 = new LiftQueue(4, 8, "Löpar kö");
            LiftQueue ko5 = new LiftQueue(4, 8, "Transport kö");

            Lift lift1 = new Lift(450, "Storaliften");
            Lift lift2 = new Lift(240, "Mellanliften");
            Lift lift3 = new Lift(600, "Springliften");
            Lift lift35 = new Lift(250, "Sido lift nedre");
            Lift lift36 = new Lift(350, "Sido lift övre");
            Lift lift4 = new Lift(500, "Ligma Liften");
            Lift lift5 = new Lift(720, "Transportliften"); //släpplift?

            Slope backe1 = new Slope(150, "Storabacken", 0.33);
            Slope backe2 = new Slope(80, "Mellanbacken", 0.66);
            Slope backe3 = new Slope(100, "Sneabacken", 1);
            Slope backe46 = new Slope(200, "Mittenbacken övre", 0.33);
            Slope backe45 = new Slope(200, "Mittenbacken nedre", 0.33);
            Slope backe5 = new Slope(170, "IvägBacken", 0.33);
            Slope backe6 = new Slope(150, "Bortrebacken", 0.33);
            Slope backe7 = new Slope(200, "Tillbakabacken", 0);
            Slope backe8 = new Slope(150, "TransportBacken", 0.33);
            Slope backe9 = new Slope(50, "SkarBacken", 0.66);

            home1.possibleMovements.Add(new Connection(valley1));

            home2.possibleMovements.Add(new Connection(valley3));

            valley1.possibleMovements.Add(new Connection(ko1));
            valley1.possibleMovements.Add(new Connection(backe9));
            valley1.possibleMovements.Add(new Connection(restaurant3));
            valley1.possibleMovements.Add(new Connection(home1));

            valley2.possibleMovements.Add(new Connection(ko3));
            valley2.possibleMovements.Add(new Connection(ko35));
            valley2.possibleMovements.Add(new Connection(ko4));
            valley2.possibleMovements.Add(new Connection(restaurant4));

            valley3.possibleMovements.Add(new Connection(ko5));
            valley3.possibleMovements.Add(new Connection(home2));
            valley3.possibleMovements.Add(new Connection(restaurant5));

            berg1.possibleMovements.Add(new Connection(backe1));
            berg1.possibleMovements.Add(new Connection(backe3));
            berg1.possibleMovements.Add(new Connection(ko2));
            berg1.possibleMovements.Add(new Connection(restaurant1));

            berg2.possibleMovements.Add(new Connection(backe2));
            berg2.possibleMovements.Add(new Connection(backe46));
            berg2.possibleMovements.Add(new Connection(restaurant6));

            berg3.possibleMovements.Add(new Connection(backe5));
            berg3.possibleMovements.Add(new Connection(backe6));
            berg3.possibleMovements.Add(new Connection(restaurant2));

            berg4.possibleMovements.Add(new Connection(backe7));
            berg4.possibleMovements.Add(new Connection(backe8));
            berg4.possibleMovements.Add(new Connection(restaurant7));

            ko1.possibleMovements.Add(new Connection(lift1));
            ko2.possibleMovements.Add(new Connection(lift2));
            ko3.possibleMovements.Add(new Connection(lift3));
            ko35.possibleMovements.Add(new Connection(lift35));
            ko4.possibleMovements.Add(new Connection(lift4));
            ko5.possibleMovements.Add(new Connection(lift5));

            lift1.possibleMovements.Add(new Connection(berg1));
            lift2.possibleMovements.Add(new Connection(berg2));
            lift3.possibleMovements.Add(new Connection(berg2));

            lift35.possibleMovements.Add(new Connection(lift36));
            lift35.possibleMovements.Add(new Connection(backe45));

            lift36.possibleMovements.Add(new Connection(berg2));

            lift4.possibleMovements.Add(new Connection(berg3));
            lift5.possibleMovements.Add(new Connection(berg4));

            backe1.possibleMovements.Add(new Connection(valley1));
            backe2.possibleMovements.Add(new Connection(berg1));
            backe3.possibleMovements.Add(new Connection(valley2));
            backe46.possibleMovements.Add(new Connection(backe45));
            backe45.possibleMovements.Add(new Connection(valley2));
            backe5.possibleMovements.Add(new Connection(valley2));
            backe6.possibleMovements.Add(new Connection(valley3));
            backe7.possibleMovements.Add(new Connection(valley3));
            backe8.possibleMovements.Add(new Connection(valley1));
            backe9.possibleMovements.Add(new Connection(valley2));

            restaurant1.possibleMovements.Add(new Connection(berg1));
            restaurant2.possibleMovements.Add(new Connection(berg3));
            restaurant3.possibleMovements.Add(new Connection(valley1));
            restaurant4.possibleMovements.Add(new Connection(valley2));
            restaurant5.possibleMovements.Add(new Connection(valley3));
            restaurant6.possibleMovements.Add(new Connection(berg2));
            restaurant7.possibleMovements.Add(new Connection(berg4));

            allLocations.Add(home1);
            allLocations.Add(home2);
            allLocations.Add(valley1);
            allLocations.Add(valley2);
            allLocations.Add(valley3);
            allLocations.Add(berg1);
            allLocations.Add(berg2);
            allLocations.Add(berg3);
            allLocations.Add(berg4);
            allLocations.Add(ko1);
            allLocations.Add(ko2);
            allLocations.Add(ko3);
            allLocations.Add(ko35);
            allLocations.Add(ko4);
            allLocations.Add(ko5);
            allLocations.Add(lift1);
            allLocations.Add(lift2);
            allLocations.Add(lift3);
            allLocations.Add(lift35);
            allLocations.Add(lift36);
            allLocations.Add(lift4);
            allLocations.Add(lift5);
            allLocations.Add(backe1);
            allLocations.Add(backe2);
            allLocations.Add(backe3);
            allLocations.Add(backe45);
            allLocations.Add(backe46);
            allLocations.Add(backe5);
            allLocations.Add(backe6);
            allLocations.Add(backe7);
            allLocations.Add(backe8);
            allLocations.Add(backe9);
            allLocations.Add(restaurant1);
            allLocations.Add(restaurant2);
            allLocations.Add(restaurant3);
            allLocations.Add(restaurant4);
            allLocations.Add(restaurant5);
            allLocations.Add(restaurant6);
            allLocations.Add(restaurant7);

            return (allLocations, allOccupants);
        }

        public static void PrintConsoleData()
        {
            //Print location history of person 5
            Console.WriteLine(allOccupants[5].name);
            foreach (Tuple<Location, int> i in allOccupants[5].locationHistory)
            {
                Console.WriteLine("{0,25:N0} {1, 20:N0}", i.Item1.name, i.Item2);
            }
            Console.Write("\n");

            //Print value of all doubles (attributes) of person 5
            foreach (FieldInfo info in typeof(Person).GetFields())
            {
                if (info.GetValue(allOccupants[5]) is double d)
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

            foreach (Home r in allLocations.Where(x => x is Home))
            {
                if (r.hungers.Count != 0)
                {
                    Console.WriteLine("{0}: average tired was: {1} with {2} entries", r.name, r.hungers.Average(), r.hungers.Count);
                }
            }

            Person p = new Person(5);
            LiftQueue re = new LiftQueue(2,2, "test");
            int length = 100;
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("{0}: {1}", re.occupants.Count, p.WeightQueueLenght(100, new Decision(re, 0), length));
                re.occupants.Add(p);
            }
        }
    }
}
