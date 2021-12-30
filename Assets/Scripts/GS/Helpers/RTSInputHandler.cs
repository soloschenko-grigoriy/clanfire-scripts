using UnityEngine;
using UnityEngine.UI;

namespace GS
{
    [RequireComponent(typeof(Camera))]
    public class RTSInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private int maxHitDistance = 500;
        [SerializeField] private RectTransform selectionBoxImage;

        private static int GroundLayer = 6; 
        private static int NotWalkableLayer = 8; 
        
        private Camera _camera;
        private Vector2 _selectionBoxStartPosition;
        private Vector2 _selectionBoxEndPosition;
        private Rect _selectionBox;
        private bool _dragStarted;
        private bool _hitGround;
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }
        
        private void Update()
        {
            OnMouseOver();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnEscPressed();
            }

            OnClick();
            TestDrag();
        }

        private void OnClick()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                OnMouseLeftClickAndShift(Input.mousePosition);
            }
            else
            {
                OnMouseLeftClick(Input.mousePosition);
            }
        }
        
        private void TestDrag()
        {
            if (Input.GetMouseButtonDown(0) && _hitGround && !playerController.UnitsManager.IsAnySelected)
            {
                playerController.UnitsManager.DeselectAll();
                _selectionBoxStartPosition = Input.mousePosition;
                _dragStarted = true;
            }

            if (Input.GetMouseButton(0) && _dragStarted)
            {
                _selectionBoxEndPosition = Input.mousePosition;
                DrawSelectionBox();
                DetectSelectionBox();
            }

            if (Input.GetMouseButtonUp(0) && _dragStarted)
            {
                _selectionBoxStartPosition = Vector2.zero;
                _selectionBoxEndPosition = Vector2.zero;
                DrawSelectionBox();
                SelectUnitsInBounds(_selectionBox);
                _dragStarted = false;
            }
        }

        private void OnMouseOver()
        {
            if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit, maxHitDistance))
            {
                return;
            }
            
            if (hit.collider.CompareTag("Unit"))
            {
                playerController.UnitHovered(hit);
            }
            else
            {
                playerController.UnitUnHovered();
            }
            
            if (hit.collider.gameObject.layer == NotWalkableLayer)
            {
                playerController.UnAccessibleHovered(hit);
            }
            else
            {
                playerController.UnAccessibleUnHovered();
            }
        }

        private void OnMouseLeftClickAndShift(Vector3 target)
        {
            if (!Physics.Raycast(_camera.ScreenPointToRay(target), out var hit, maxHitDistance))
            {
                return;
            }
            
            if (hit.collider.CompareTag("Unit"))
            {
                playerController.UnitClicked(hit, true);
            }
        }

        private void OnMouseLeftClick(Vector3 target)
        {
            _hitGround = false;
            if (!Physics.Raycast(_camera.ScreenPointToRay(target), out var hit, maxHitDistance))
            {
                return;
            }

            if (hit.collider.CompareTag("Unit"))
            {
                playerController.UnitClicked(hit, false);
            }
            else if (hit.collider.gameObject.layer == GroundLayer)
            {
                _hitGround = true;
                playerController.GroundClicked(hit);
            }
            else if (hit.collider.gameObject.layer == NotWalkableLayer)
            {
                playerController.UnknownClicked(hit);
            }
        }
        
        private void SelectUnitsInBounds(Rect selectionBox)
        {
            foreach (var unit in playerController.UnitsManager.Units)
            {
                if (_selectionBox.Contains(_camera.WorldToScreenPoint(unit.transform.position)))
                {
                    playerController.UnitsManager.Select(unit, true);
                }
            }
        }
        
        private void OnEscPressed()
        {
            playerController.CancelAllSelections();
        }

        private void DrawSelectionBox()
        {
            var center = (_selectionBoxStartPosition + _selectionBoxEndPosition) / 2;
            var size = new Vector2(
                Mathf.Abs(_selectionBoxStartPosition.x - _selectionBoxEndPosition.x),
                Mathf.Abs(_selectionBoxStartPosition.y - _selectionBoxEndPosition.y)
            );
            
            selectionBoxImage.position = center;
            selectionBoxImage.sizeDelta = size;
        }

        private void DetectSelectionBox()
        {
            // Drag left
            if (Input.mousePosition.x < _selectionBoxStartPosition.x)
            {
                _selectionBox.xMin = Input.mousePosition.x;
                _selectionBox.xMax = _selectionBoxStartPosition.x;
            }
            else // Drag right
            {
                _selectionBox.xMin = _selectionBoxStartPosition.x;
                _selectionBox.xMax = Input.mousePosition.x;
            }

            // Drag down
            if (Input.mousePosition.y < _selectionBoxStartPosition.y)
            {
                _selectionBox.yMin =  Input.mousePosition.y;
                _selectionBox.yMax = _selectionBoxStartPosition.y;
            }
            else // Drag up
            {
                _selectionBox.yMin = _selectionBoxStartPosition.y;
                _selectionBox.yMax = Input.mousePosition.y;
            }
        }
    }
}
