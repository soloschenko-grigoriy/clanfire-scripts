using UnityEngine;
using Random = UnityEngine.Random;

namespace GS.Units
{
    public enum UnitAnimationState
    {
        Idle,
        Moving,
        Attacking,
        Dying,
    }
    
    public class UnitAnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int ANIMATOR_RUN = Animator.StringToHash("Run");
        private static readonly int ANIMATOR_ATTACK_A = Animator.StringToHash("AttackA");
        private static readonly int ANIMATOR_ATTACK_B = Animator.StringToHash("AttackB");
        private static readonly int ANIMATOR_DEATH_A = Animator.StringToHash("DeathA");
        private static readonly int ANIMATOR_DEATH_B = Animator.StringToHash("DeathB");
        
        private UnitAnimationState _state;

        public void ChangeState(UnitAnimationState newState)
        {
            TurnOffAllAnimatorStates();

            switch (newState)
            {
                case UnitAnimationState.Moving:
                    animator.SetBool(ANIMATOR_RUN, true);
                    break;
                case UnitAnimationState.Attacking:
                    animator.SetBool( Random.Range(0, 1) > 0.5 ? ANIMATOR_ATTACK_A :  ANIMATOR_ATTACK_B, true);
                    break;
                case UnitAnimationState.Dying:
                    animator.SetBool( Random.Range(0, 1) > 0.5 ? ANIMATOR_DEATH_A :  ANIMATOR_DEATH_B, true);
                    break;
            }

            _state = newState;
        }
        
        private void TurnOffAllAnimatorStates()
        {
            animator.SetBool(ANIMATOR_RUN, false);
            animator.SetBool(ANIMATOR_ATTACK_A, false);
            animator.SetBool(ANIMATOR_ATTACK_B, false);
            animator.SetBool(ANIMATOR_DEATH_A, false);
            animator.SetBool(ANIMATOR_DEATH_B, false);
        }
    }
}
