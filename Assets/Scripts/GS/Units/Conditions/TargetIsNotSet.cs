using UnityEngine;
using GS.StateMachina;

namespace GS.Units.Conditions
{
    [CreateAssetMenu(fileName = "Unit's Target Is Not Set", menuName = "State Machine / Conditions / Unit's Target Is Not Set")]
    public class TargetIsNotSet: Condition
    {
        public override bool Check(StateMachine stateMachine)
        {
            return !stateMachine.GetComponent<UnitActivity>().TargetIsSet;
        }
    }
}
