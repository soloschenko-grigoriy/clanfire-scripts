using GS.StateMachina;
using UnityEngine;

namespace GS.Units.Actions
{
    [CreateAssetMenu(fileName = "Unit Show Dying Animation", menuName = "State Machine / Actions / Unit Show Dying Animation")]
    public class UnitShowDyingAnimation : Action
    {
        public override void Act(StateMachine stateMachine)
        {
            stateMachine.GetComponent<UnitAnimationManager>().ChangeState(UnitAnimationState.Dying);
        }
    }
}
