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

        static public void UpdateSystem(int timeStep, List<Location> allLocations)//This function is hilariously poorly implemented, but (...)Move method isn't virtual
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
                foreach(Location l in allLocations)
                {
                    if(l.GetType().Name == s)
                    {
                        string methodName = s + "Move";
                        l.GetType().GetMethod(methodName).Invoke(l, new object[] { timeStep });
                        debugCounter++;
                    }
                }
            }
            if (debugCounter != debugCheck)
            {
                Console.WriteLine("Amount of Locations didn't add up! UpdateSystem method");
            }

            //foreach ( Location l in allLocations)
            //{
            //    string methodName = l.GetType().Name + "Move";
            //    l.GetType().GetMethod(MethodName).Invoke(l, new object[] { timeStep });
            //}

            //foreach(Location i in allLocations)
            //{
            //    if(i is Lift j)
            //    {
            //        j.LiftMove(timeStep);
            //        debugCounter++;

            //    }
            //}
            //foreach(Location i in allLocations)
            //{
            //    if (i is Slope j)
            //    {
            //        j.SlopeMove(timeStep);
            //        debugCounter++;
            //    }
            //}
            //foreach(Location i in allLocations)
            //{
            //    if (i is LiftQueue j)
            //    {
            //        j.LiftQueueMove(timeStep);
            //        debugCounter++;
            //    }
            //}
            //foreach(Location i in allLocations)
            //{
            //    if (i is Restaurant j)
            //    {
            //        j.RestaurantMove(timeStep);
            //        debugCounter++;
            //    }
            //}
            //foreach(Location i in allLocations)
            //{
            //    if (i is MountainTop j)
            //    {
            //        j.MountainTopMove(timeStep);
            //        debugCounter++;
            //    }
            //}
            //foreach(Location i in allLocations)
            //{
            //    if (i is Valley j)
            //    {
            //        j.ValleyMove(timeStep);
            //        debugCounter++;
            //    }
            //}

        }

        static public string NameGenerator() //Don't add more names to files
        {


            StreamReader firstNames = new StreamReader("Files\\FirstNames.txt");
            int numberOfFirstNames = 392;


            
            StreamReader lastNames = new StreamReader("Files\\LastNames.txt");
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
