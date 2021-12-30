using GS.StateMachina;
using UnityEngine;

namespace GS.Units.Conditions
{
    [CreateAssetMenu(fileName = "Unit's Attack Is Not Ready", menuName = "State Machine / Conditions / Unit's Attack Is Not Ready")]
    public class AttackIsNotReady:Condition
    {
        public override bool Check(StateMachine stateMachine)
        {
            return stateMachine.GetComponent<UnitAttackManager>().IsRecharging;
        }
    }
}
