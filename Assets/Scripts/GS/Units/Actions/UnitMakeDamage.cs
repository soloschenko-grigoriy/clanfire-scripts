using GS.StateMachina;
using UnityEngine;

namespace GS.Units.Actions
{
    [CreateAssetMenu(fileName = "Unit Make Damage", menuName = "State Machine / Actions / Unit Make Damage")]
    public class UnitMakeDamage: Action
    {
        public override void Act(StateMachine stateMachine)
        {
            var target = stateMachine.GetComponent<UnitActivity>().Target;
            stateMachine.GetComponent<UnitAttackManager>().Attack(target);
        }
    }
}
