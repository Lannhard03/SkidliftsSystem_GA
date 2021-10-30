using System;

namespace SkidliftSys
{
    class Person
    {
        string name; //for estetic?
        int person_number; //for keeping track of who is where.

        int morningness; //from 1-100? How early do they start skiing.
        int hungryness;  //from 1-100? how hungry they are.
        int skill_level; //from 1-100? How good at skiing is the person.


        bool hunger_state; //if true they want to find a restaurant
        bool done_skiing_state; //if true they want to go home
    }
}
