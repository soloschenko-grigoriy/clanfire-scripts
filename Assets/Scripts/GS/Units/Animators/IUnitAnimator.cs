namespace GS.Units.Animators
{
    public interface IUnitAnimator
    {
        UnitAnimatorState State { get; }
        void Setup();
        void ChangeState(UnitAnimatorState newState);
    }
}
