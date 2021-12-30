using UnityEngine;

namespace GS.StateMachina
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool Check(StateMachine stateMachine);
    }
}
