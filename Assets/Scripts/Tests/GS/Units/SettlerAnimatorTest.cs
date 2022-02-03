using GS.Units.Animators;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GS.Units
{
    public class SettlerAnimatorTest
    {
        private IUnitAnimator _settlerAnimator;
        private Animator _animator;

        [SetUp]
        public void Setup()
        {
            var unit = TestUtils.InitUnit("Settler.prefab");
            
            _settlerAnimator = unit.GetComponent<IUnitAnimator>();
            _animator = unit.GetComponentInChildren<Animator>();
            
            _settlerAnimator.Setup();
        }


        [TearDown]
        public void TearDown()
        {
            _settlerAnimator = null;
            _animator = null;
        }

        [Test]
        public void ChangesStateAndAnimationVarToMovingWhenSet()
        {
            _settlerAnimator.ChangeState(UnitAnimatorState.Moving);
            
            Assert.That(_settlerAnimator.State, Is.EqualTo(UnitAnimatorState.Moving));
            Assert.IsTrue(_animator.GetBool("Run"));
        }
        
        [Test]
        public void ChangesStateAndAnimationVarToDeathWhenSet()
        {
            _settlerAnimator.ChangeState(UnitAnimatorState.Dying);
            
            Assert.That(_settlerAnimator.State, Is.EqualTo(UnitAnimatorState.Dying));
            Assert.IsTrue(_animator.GetBool("Death"));
        }
        
        [Test]
        public void DoesNotSupportAttackState()
        {
            _settlerAnimator.ChangeState(UnitAnimatorState.Attacking);
            
            Assert.That(_settlerAnimator.State, Is.EqualTo(UnitAnimatorState.Idle));
            Assert.IsFalse(_animator.GetBool("Run"));
            Assert.IsFalse(_animator.GetBool("Death"));
        }
    }
}
