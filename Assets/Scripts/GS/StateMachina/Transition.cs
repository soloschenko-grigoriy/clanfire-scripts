using System;

namespace GS.StateMachina
{
    [Serializable]
    public class Transition
    {
        public State nextState;
        public Condition[] conditions;
    }
}
