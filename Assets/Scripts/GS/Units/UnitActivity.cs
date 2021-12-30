using System;
using UnityEditor;
using UnityEngine;

namespace GS.Units
{
    [RequireComponent(typeof(Unit))]
    public class UnitActivity : MonoBehaviour
    {
        private Unit _unit;
        private IUnitTarget _target;
        private bool _allowLoosingTarget;
        private bool _targetIsSet;
        private bool _targetInReach;

        public IUnitTarget Target => _target;
        public bool TargetIsSet => _targetIsSet;
        public bool TargetInReach => _targetInReach;
        public Vector3 TargetCurrentPosition => _target.Position;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
        }

        private void Update()
        {
            CheckIfTargetStillAvailable();
            CheckIfTargetIsInReach();
        }
        
        public void SetTarget(IUnitTarget target, bool allowLoosingTarget)
        {
            _target = target;
            _targetIsSet = true;
            _allowLoosingTarget = allowLoosingTarget;
        }

        public void RemoveTarget(bool stopMarching)
        {
            if (stopMarching)
            {
                _unit.StopMarching();
            }
            
            _targetIsSet = false;
            _targetInReach = false;
            _allowLoosingTarget = false;
            _target = null;
        }

        private void CheckIfTargetStillAvailable()
        {
            if (!_targetIsSet)
            {
                return;
            }

            if (_target.IsAvailable)
            {
                return;
            }

            RemoveTarget(true);
        }

        private void CheckIfTargetIsInReach()
        {
            if (!_targetIsSet)
            {
                return;
            }

            var dist = (_unit.Position - _target.Position).magnitude;
            _targetInReach = dist <= _unit.Config.TargetReachDistance;

            if (_allowLoosingTarget && dist > _unit.Config.TargetGiveUpDistance)
            {
                RemoveTarget(true);
            }
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_target != null)
            {
                Debug.DrawLine(transform.position, _target.Position, Color.blue);
            }
            
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(transform.position, transform.up, _unit.Config.TargetReachDistance);
        }
#endif
    }
}
