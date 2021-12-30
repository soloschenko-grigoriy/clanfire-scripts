using UnityEngine;

namespace GS.StateMachina
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private State initState;

        private State _currentState;

        private void Awake()
        {
            _currentState = initState;
        }

        private void Update()
        {
            _currentState.OnUpdateState(this);
        }

        public void TransitionTo(State nextState)
        {
            _currentState.OnExitState(this);
            _currentState = nextState;
            _currentState.OnEnterState(this);
        }
    }
}
