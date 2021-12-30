using GS.Hex;
using GS.Structures;
using GS.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GS
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private ConfirmNewTown confirmPopup;
        [SerializeField] private HexGrid grid;
        [SerializeField] private int maxHitDistance = 500;
        
        // TODO this should not be here
        [SerializeField] private Structure townHallPrefab;
        
        private const int GroundLayerMask = 1 << 6; 

        private Camera _camera;
        
        // TODO this should not be here
        private bool _confirmOpen;
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }
        
        private void Update()
        {
            if (_confirmOpen)
            {
                return;
            }
            
            TestOnClick();
        }
        
        private void TestOnClick()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            OnMouseLeftClick(Input.mousePosition);
        }

        private void OnMouseLeftClick(Vector3 target)
        {
            if (!Physics.Raycast(_camera.ScreenPointToRay(target), out var hit, maxHitDistance, GroundLayerMask))
            {
                return;
            }

            var cell = grid.GetCell(hit.point);
            if (!cell)
            {
                return;
            }
            
            if (cell.Category == HexCellCategory.Building && cell.HasContents)
            {
                cell.Contents.GetComponent<Structure>().OnTouch();
                return;
            }
            
            if (!cell.CanContainBuildings())
            {
                return;
            }

            _confirmOpen = true;
            confirmPopup.Show(() => {
                _confirmOpen = false;
                cell.SetContents(townHallPrefab, HexCellObjectType.Building, false);
                cell.SetCategory(HexCellCategory.Building);
                cell.Chunk.Refresh();
            }, () => {
                _confirmOpen = false;
            });
        }
    }
}
