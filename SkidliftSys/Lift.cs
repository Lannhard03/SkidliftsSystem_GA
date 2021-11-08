using System;
using System.Collections.Generic;
using System.Linq;

namespace SkidliftSys
{
    class Lift : Location
    {
        //You may exit certain lifts in the middle, then the person makes a decision to continue (enter the next lift) or leave.
        int timelength;
        int maxoccupants; //if lift has this many people, don't add more.
        public Lift(List<Person> people, int lenghttime)
        {
            occupants = people;
            timelength = lenghttime;
        }
        public Lift(int lenghttime)
        {
            timelength = lenghttime;
        }
        public void MoveLift(int timestep)
        {
            List<Person> movingpeople = new List<Person>(); //temp
            foreach (Person i in occupants)
            {
                i.time_location += timestep; //increase the time in the location by timestep and move person if nessecary.
                if (i.time_location >= timelength)
                {
                    movingpeople.Add(i);
                }
            }
            movingpeople.Sort((x,y) => y.time_location.CompareTo(x.time_location)); //note that x,y change places, then we sort from highest to lowest (with respect to time_location)
            
            foreach(Person i in movingpeople)
            {
                i.DecisionHandler(possiblemovements, this).AddPerson(i); //Person i makes a decision and moves there.
                RemovePerson(i);
            }
        }
       





    }
}
