using UnityEngine;

namespace GS.StateMachina
{
    public abstract class Action: ScriptableObject
    {
        public abstract void Act(StateMachine stateMachine);
    }
}
