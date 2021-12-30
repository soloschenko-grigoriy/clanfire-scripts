using UnityEngine;
using GS.StateMachina;

namespace GS.Units.Conditions
{
    [CreateAssetMenu(fileName = "Unit's Health Is Zero", menuName = "State Machine / Conditions / Unit's Health Is Zero")]
    public class HealthIsZero: Condition
    {
        public override bool Check(StateMachine stateMachine)
        {
            return stateMachine.GetComponent<Unit>().Health <= 0;
        }
    }
}
