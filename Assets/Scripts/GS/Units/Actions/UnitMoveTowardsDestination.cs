using GS.StateMachina;
using UnityEngine;

namespace GS.Units.Actions
{
    [CreateAssetMenu(fileName = "Unit Move Towards Dest", menuName = "State Machine / Actions / Unit Move Towards Dest")]
    public class UnitMoveTowardsDestination: Action
    {
        public override void Act(StateMachine stateMachine)
        {
            stateMachine.GetComponent<UnitMovement>().Go();
        }
    }
}
