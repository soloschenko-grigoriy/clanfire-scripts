using UnityEngine;
using GS.StateMachina;

namespace GS.Units.Conditions
{
    [CreateAssetMenu(fileName = "Destination Is Not Set", menuName = "State Machine / Conditions / Unit's Destination Is Not Set")]
    public class DestinationIsNotSet: Condition
    {
        public override bool Check(StateMachine stateMachine)
        {
            return !stateMachine.GetComponent<UnitMovement>().DestinationIsSet;
        }
    }
}
