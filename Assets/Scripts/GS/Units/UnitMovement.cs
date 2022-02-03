using System;
using System.Linq;
using GS.Hex;
using GS.Units.Animators;
using UnityEngine;

namespace GS.Units
{
    public class UnitMovement : MonoBehaviour
    {
        public bool InProgress => _inProgress;
        
        private bool _inProgress;
        private float _timeStarted;
        private float _duration;

        private IUnit _unit;
        private IHexCell _currentCell;
        private IHexCell _nextCell;
        
        // TODO Temp here
        private IUnitAnimator _unitAnimator;

        private void OnEnable()
        {
            _unitAnimator = GetComponent<IUnitAnimator>();
        }

        public void StartMoving(IUnit unit, float duration)
        {
            if (_inProgress)
            {
                return;
            }

            _unit = unit;
            _duration = duration;
            _inProgress = true;

            ContinueToNextCell();
        }

        public void MakeProgress(float currentTime)
        {
            float progress = (currentTime - _timeStarted) / _duration;
            if (progress < 1)
            {
                _unit.Position = Vector3.Lerp(
                    _currentCell.Position,
                    _nextCell.Position,
                    progress
                );
                return;
            }

            ContinueToNextCell();
        }
        
        private void ContinueToNextCell()
        {
            if (_nextCell != null)
            {
                _unit.SetCell(_nextCell);
            }

            CleanupCurrentCell();

            if (_unit.CurrentPath.Count < 1)
            {
                CompleteMovement();
                return;
            }

            SetupNextCell();

            _timeStarted = Time.time;
            transform.LookAt(_nextCell.Position);
        }
        
        private void CleanupCurrentCell()
        {
            _currentCell = _unit.Cell;
            _currentCell.IsOnPath = false;
            _currentCell.IsHighlightedAsDestination = false;
            _currentCell.Refresh();
        }

        private void SetupNextCell()
        {
            _nextCell = _unit.CurrentPath.Last();
            _unit.CurrentPath.Remove(_nextCell);
        }

        private void CompleteMovement()
        {
            _inProgress = false;
            _unit.ResetDestination();
            
            // TODO Should be controlled by state machine directly
            _unitAnimator.ChangeState(UnitAnimatorState.Idle);
        }
    }
}
