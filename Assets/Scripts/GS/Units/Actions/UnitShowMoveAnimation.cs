using GS.StateMachina;
using UnityEngine;

namespace GS.Units.Actions
{
    [CreateAssetMenu(fileName = "Unit Show Move Animation", menuName = "State Machine / Actions / Unit Show Move Animation")]
    public class UnitShowMoveAnimation : Action
    {
        public override void Act(StateMachine stateMachine)
        {
            stateMachine.GetComponent<UnitAnimationManager>().ChangeState(UnitAnimationState.Moving);
        }
    }
}
