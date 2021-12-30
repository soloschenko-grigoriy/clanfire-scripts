using System.Linq;
using UnityEngine;

namespace GS.StateMachina
{
    [CreateAssetMenu(fileName = "State", menuName = "State Machine / State")]
    public class State : ScriptableObject
    {
        [SerializeField] private Action[] enterActions;
        [SerializeField] private Action[] updateActions;
        [SerializeField] private Action[] exitActions;
        [SerializeField] private Transition[] transitions;

        public void OnEnterState(StateMachine stateMachine)
        {
            DoActions(enterActions, stateMachine);
        }
        
        public void OnUpdateState(StateMachine stateMachine)
        {
            DoActions(updateActions, stateMachine);
            CheckTransitions(stateMachine);
        }
        
        public void OnExitState(StateMachine stateMachine)
        {
            DoActions(exitActions, stateMachine);
        }
        
        private void CheckTransitions(StateMachine stateMachine)
        {
            foreach (var transition in transitions)
            {
                if (!transition.conditions.All((condition => condition.Check(stateMachine))))
                {
                    continue;
                }

                stateMachine.TransitionTo(transition.nextState);
                break;
            }
        }
        
        private static void DoActions(Action[] actions, StateMachine stateMachine)
        {
            foreach (var action in actions)
            {
                action.Act(stateMachine);
            }
        }
    }
}
