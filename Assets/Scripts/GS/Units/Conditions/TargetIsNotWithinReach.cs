using UnityEngine;
using GS.StateMachina;

namespace GS.Units.Conditions
{
    [CreateAssetMenu(fileName = "Unit's Target Is Not Within Reach", menuName = "State Machine / Conditions / Unit's Target Is Not Within Reach")]
    public class TargetIsNotWithinReach: Condition
    {
        public override bool Check(StateMachine stateMachine)
        {
            return !stateMachine.GetComponent<UnitActivity>().TargetInReach;
        }
    }
}
