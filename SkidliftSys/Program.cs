using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

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

        static public void UpdateSystem(int timeStep, List<Location> allLocations)
        {
            //we would like to update ceratain types of places before others.
            //Restaurant (and places that are full last) movement last
            //uppdate connections: Lifts may open restaurants may no longer be full


            
            foreach (Location i in allLocations)
            {
                switch (i) 
                {
                    case Lift j:
                        j.LiftMove(timeStep);
                        break;
                    case Slope j:
                        j.SlopeMove(timeStep);
                        break;
                    case LiftQueue j:
                        j.LiftQueueMove(timeStep);
                        break;
                    case Restaurant j:
                        j.RestaurantMove(timeStep);
                        break;
                    case MountainTop j:
                        j.TopOfMountainMove(timeStep);
                        break;
                    default:
                        throw new ArgumentException("Invalid Location");
                }
                
            }

        }

        static public string NameGenerator() //Don't add more names to files
        {
            string outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string iconPath = Path.Combine(outputDirectory, "Files\\FirstNames.txt");
            string icon_path = new Uri(iconPath).LocalPath;

            StreamReader firstNames = new StreamReader(icon_path);
            int numberOfFirstNames = 392;

            outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            iconPath = Path.Combine(outputDirectory, "Files\\LastNames.txt");
            icon_path = new Uri(iconPath).LocalPath;
            
            StreamReader lastNames = new StreamReader(icon_path);
            int numberOfLastNames = 203;


            Random rnd = new Random();
            for(int i = 0; i< rnd.Next(0, numberOfFirstNames); i++)
            {
                string temp = firstNames.ReadLine();
                
            }
            for (int i = 0; i < rnd.Next(0, numberOfLastNames); i++)
            {
                string temp = lastNames.ReadLine();
                
            }

            string name = firstNames.ReadLine() + lastNames.ReadLine();

            firstNames.Close();
            lastNames.Close();

            return name;
        }
    }
}
