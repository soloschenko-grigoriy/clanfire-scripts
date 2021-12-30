using GS.StateMachina;
using UnityEngine;

namespace GS.Units.Actions
{
    [CreateAssetMenu(fileName = "Unit Stop", menuName = "State Machine / Actions / Unit Stop")]
    public class UnitStop: Action
    {
        public override void Act(StateMachine stateMachine)
        {
            stateMachine.GetComponent<UnitMovement>().Stop();
        }
    }
}
