using UnityEngine;
using GS.StateMachina;

namespace GS.Units.Conditions
{
    [CreateAssetMenu(fileName = "Unit's Destination Is Set", menuName = "State Machine / Conditions / Unit's Destination Is Set")]
    public class DestinationIsSet: Condition
    {
        public override bool Check(StateMachine stateMachine)
        {
            return stateMachine.GetComponent<UnitMovement>().DestinationIsSet;
        }
    }
}
