using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    public abstract class Location
    {
        public List<Person> occupants = new List<Person>();
        public void RemovePeople(List<Person> people)
        {
            foreach (Person i in people)
            {
                occupants.Remove(i);
            }
        }

        public void AddPeople(List<Person> people)
        {
            foreach (Person i in people)
            {
                occupants.Add(i);
            }
        }

    }
}
