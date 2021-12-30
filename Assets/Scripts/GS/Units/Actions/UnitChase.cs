using GS.StateMachina;
using UnityEngine;

namespace GS.Units.Actions
{
    [CreateAssetMenu(fileName = "Unit to Chase", menuName = "State Machine / Actions / Unit to Chase")]
    public class UnitChase: Action
    {
        public override void Act(StateMachine stateMachine)
        {
            var unit = stateMachine.GetComponent<Unit>();
            var unitManager = unit.Manager;
            var activity = stateMachine.GetComponent<UnitActivity>();
            var unitMovement = stateMachine.GetComponent<UnitMovement>();

            if (!activity.TargetIsSet)
            {
                return;
            }

            var targetPos = activity.TargetCurrentPosition;
            var dir = (unit.Position - targetPos).normalized;
            var diff = dir * 0.5f;

            unitMovement.SetDestination(targetPos + diff, unitManager.SelectedUnits.Count);
            unitMovement.Go();
        }
    }
}
