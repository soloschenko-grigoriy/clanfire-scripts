using System.Collections.Generic;
using GS.Hex;
using GS.Players;
using GS.Units.Animators;
using UnityEngine;

namespace GS.Units
{
    public class Unit : MonoBehaviour, IUnit
    {
        [SerializeField] private GameObject selectedPointer;
        [SerializeField] private UnitConfig config;
        
        public IHexCell Destination => _destination;
        public List<IHexCell> CurrentPath => _currentPath;
        public IHexCell Cell => _cell;
        public GameObject GameObject => gameObject;
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        private HexCell _cell;
        private bool _isSelected;
        private bool _movementInProgress;
        private HexCell _destination;
        private UnitMovement _movement;
        private IUnitAnimator _unitAnimator;
        private List<IHexCell> _accessibleCells = new List<IHexCell>();
        private List<IHexCell> _currentPath = new List<IHexCell>();
        private PlayerBehaviour _playerBehaviour;

        private void OnEnable()
        {
            _movement ??= GetComponent<UnitMovement>();
            _unitAnimator ??= GetComponent<IUnitAnimator>();
        }

        private void Update()
        {
            if (_movement.InProgress)
            {
                _movement.MakeProgress(Time.time);
            }
        }

        public void Spawn(HexCell on, IPlayer player)
        {
            SetCell(on);
            _playerBehaviour = player.PlayerBehaviour;
        }

        public void SetCell(IHexCell cell)
        {
            // Cleanup first
            _cell?.RemoveUnit();

            _cell = cell as HexCell;
            transform.position = cell.Position;
            
            cell.SetUnit(this);
        }

        public void TrySetDestination(IHexCell cell)
        {
            _currentPath.ForEach(c => {
                c.IsOnPath = false;
                c.Refresh();
            });
            
            var path = HexPathFinder.FindPath(_cell, cell);
            if (path.Count > 0)
            {
                _destination = cell as HexCell;
                _currentPath = path;
                
                _destination.IsHighlightedAsDestination = true;
            
                // current is not actually part of the path but still should be highlighted
                _cell.IsOnPath = true;
                _cell.Refresh();
                
                _currentPath.ForEach(c => c.IsOnPath = true);
                _currentPath.ForEach(c => c.Refresh()); // we have to do this in 2 separate calls otherwise border may not render properly
            }
        }

        public void StartMovingTowardsDestination()
        {
            if (Destination == null)
            {
                return;
            }
            
            _movement.StartMoving(this, config.MovementDuration);
            
            // TODO should be controlled by state machine independently from this
            _unitAnimator.ChangeState(UnitAnimatorState.Moving);
            
            DeSelect();
        }

        public void ResetDestination()
        {
            _destination = null;
        }

        public void Select()
        {
            _isSelected = true;
            selectedPointer.gameObject.SetActive(_isSelected);
            HighlightAccessibleHexes();
            HighlightDestination();
        }

        public void DeSelect()
        {
            _isSelected = false;
            selectedPointer.gameObject.SetActive(_isSelected);
            
            DeHighlightHexes();
            DeHighlightDestination();
        }
        
        private void HighlightDestination()
        {
            if (_destination == null)
            {
                return;
            }

            _destination.IsHighlightedAsDestination = true;
            _destination.Refresh();
        }

        private void DeHighlightDestination()
        {
            if (_destination == null)
            {
                return;
            }
            
            _destination.IsHighlightedAsDestination = false;
            _destination.Refresh();
        }

        private void HighlightAccessibleHexes()
        {
            _accessibleCells = _cell.GetAccessibleIn(config.MovementRadius);
            _accessibleCells.ForEach(cell => cell.IsHighlightedAsAccessible = true);
            _accessibleCells.ForEach(cell => cell.Refresh()); // we have to do this in 2 separate calls otherwise border may not render properly
        }

        private void DeHighlightHexes()
        {
            _accessibleCells.ForEach(cell => {
                cell.IsHighlightedAsAccessible = false;
                cell.IsOnPath = false;
                cell.ResetDistance();
                cell.Refresh();
            });
        }
    }
}
