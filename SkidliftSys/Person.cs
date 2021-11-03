using System;
using System.Collections.Generic;

namespace SkidliftSys
{
    public class Person
    {
        string name; //for estetic?
        int person_number; //for keeping track of who is where.

        int morningness; //from 0-1? How early do they start skiing.
        int hungryness;  //from 0-1? how hungry they are.
        int skill_level; //from 0-1? How good at skiing is the person.
        int explororness; //from 0-1? How much they want to visit new lifts.

        bool hunger_state; //if true they want to find a restaurant
        bool done_skiing_state; //if true they want to go home

        int time_location; //It is useful to know how long someone has been in a lift


        List<Decision> future_decisions = new List<Decision>(); // do decisions require there own class?
        List<Location> location_history = new List<Location>();
        

      
    }
}
