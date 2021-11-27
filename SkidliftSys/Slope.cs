﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SkidliftSys
{
    class Slope : Location
    {
        
        int slopeTime;
        public int difficulty; //1,2,3,4 -> green, blue, red, black
        public Slope(List<Person> occupants, int slopeTime, string name)
        {
            this.name = name;
            this.occupants = occupants;
            this.slopeTime = slopeTime;
            this.difficulty = 2;
        }
        public Slope(int slopeTime, string name)
        {
            this.name = name;
            this.slopeTime = slopeTime;
            this.difficulty = 2;
        }

        public void SlopeMove(int timeStep)
        {
            List<Person> movingPeople = new List<Person>(); //temp
            foreach(Person i in occupants)
            {
                i.timeLocation += timeStep; //increase the time in the location by timestep and move person if nessecary.
                if (i.timeLocation >= slopeTime)
                {
                    movingPeople.Add(i);
                }
            }
            foreach(Person i in movingPeople) //why are there two foreach loops (and a temporary list) here when one is enough?
            {
                i.DecisionHandler(possibleMovements, this).MovePerson(i, this); //Person i makes a decision and moves there.
            }

            
        }


        

    }
}
