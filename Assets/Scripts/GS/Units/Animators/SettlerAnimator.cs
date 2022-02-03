using UnityEngine;

namespace GS.Units.Animators
{
    public class SettlerAnimator: MonoBehaviour, IUnitAnimator
    {
        public UnitAnimatorState State => _state;
        
        private static readonly int ANIMATOR_MOVE = Animator.StringToHash("Run");
        private static readonly int ANIMATOR_DEATH = Animator.StringToHash("Death");

        private UnitAnimatorState _state;
        private Animator _animator;

        private void Awake()
        {
            Setup();
        }

        public void Setup()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void ChangeState(UnitAnimatorState newState)
        {
            TurnOffAllAnimatorStates();

            var key = GetKey(newState);
            
            // smth not supported
            if (key == 0)
            {
                _state = UnitAnimatorState.Idle;
                return;
            }
            
            _animator.SetBool(key, true);
            _state = newState;
        }

        private int GetKey(UnitAnimatorState state)
        {
            return state switch {
                UnitAnimatorState.Moving => ANIMATOR_MOVE,
                UnitAnimatorState.Dying => ANIMATOR_DEATH,
                _ => 0
            };
        }
        
        private void TurnOffAllAnimatorStates()
        {
            _animator.SetBool(ANIMATOR_MOVE, false);
            _animator.SetBool(ANIMATOR_DEATH, false);
        }
    }
}
