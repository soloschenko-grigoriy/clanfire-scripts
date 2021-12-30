using GS.StateMachina;
using UnityEngine;

namespace GS.Units.Conditions
{
    [CreateAssetMenu(fileName = "Unit's Attack Is Ready", menuName = "State Machine / Conditions / Unit's Attack Is Ready")]
    public class AttackIsReady:Condition
    {
        public override bool Check(StateMachine stateMachine)
        {
            return !stateMachine.GetComponent<UnitAttackManager>().IsRecharging;
        }
    }
}
