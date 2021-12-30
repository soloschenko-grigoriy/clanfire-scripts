using GS.StateMachina;
using UnityEngine;

namespace GS.Units.Actions
{
    [CreateAssetMenu(fileName = "Unit Show Idle Animation", menuName = "State Machine / Actions / Unit Show Idle Animation")]
    public class UnitShowIdleAnimation : Action
    {
        public override void Act(StateMachine stateMachine)
        {
            stateMachine.GetComponent<UnitAnimationManager>().ChangeState(UnitAnimationState.Idle);
        }
    }
}
