using System;
using System.Collections.Generic;
using System.Text;

namespace SkidliftSys
{
    class LiftQueue
    {
        List<Person> occupants = new List<Person>();
        int liftspeed;
        List<Connections> possiblemovements = new List<Connections>(); //How to express location? Absolute location might be useful for pathfinding but how do you express it? Relative position probably enough?

        public void Liftpeople() //move people from queue to associated lift And if someone is at the top of lift/ at an exit they must/may exit the lift.
        {

        }
    }
}
