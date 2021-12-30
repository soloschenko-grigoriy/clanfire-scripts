using UnityEngine;
using GS.StateMachina;

namespace GS.Units.Conditions
{
    [CreateAssetMenu(fileName = "Unit's Target Is Within Reach", menuName = "State Machine / Conditions / Unit's Target Is Within Reach")]
    public class TargetIsWithinReach: Condition
    {
        public override bool Check(StateMachine stateMachine)
        {
            return stateMachine.GetComponent<UnitActivity>().TargetInReach;
        }
    }
}
