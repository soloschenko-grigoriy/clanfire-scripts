using GS.StateMachina;
using UnityEngine;

namespace GS.Units.Actions
{
    [CreateAssetMenu(fileName = "Unit Show Attack Animation", menuName = "State Machine / Actions / Unit Show Attack Animation")]
    public class UnitShowAttackAnimation: Action
    {
        public override void Act(StateMachine stateMachine)
        {
            stateMachine.GetComponent<UnitAnimationManager>().ChangeState(UnitAnimationState.Attacking);
        }
    }
}
