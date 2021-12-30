using UnityEngine;
using GS.StateMachina;

namespace GS.Units.Conditions
{
    [CreateAssetMenu(fileName = "Unit's Target Is Set", menuName = "State Machine / Conditions / Unit's Target Is Set")]
    public class TargetIsSet: Condition
    {
        public override bool Check(StateMachine stateMachine)
        {
            return stateMachine.GetComponent<UnitActivity>().TargetIsSet;
        }
    }
}
