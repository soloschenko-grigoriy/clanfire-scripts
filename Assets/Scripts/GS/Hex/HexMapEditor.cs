using UnityEngine;

namespace GS.Hex
{
    public class HexMapEditor : MonoBehaviour
    {
        [SerializeField] private HexGrid hexGrid;

        private Camera _camera;
        private HexCellCategory _activeType;
        private HexCellObjectType _activeCellObject;
        private Selected selected;
        private const int LayerMask = 1 << 6;

        private void Awake ()
        {
            _camera = Camera.main;
            SelectType(0);
        }

        private void Update () {
            if (Input.GetMouseButtonUp(0)) {
                HandleInput();
            }
        }

        private void HandleInput () {
            Ray inputRay = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(inputRay, out var hit, 300, LayerMask))
            {
                return;
            }

            if (selected == Selected.Type)
            {
                hexGrid.ChangeCell(hit.point, _activeType);
            } else if (selected == Selected.Object)
            {
                hexGrid.ChangeCell(hit.point, _activeCellObject);
            }
             
        }

        public void SelectType (int index) {
            selected = Selected.Type;
            _activeType = (HexCellCategory)index;
        }

        public void SelectObject(int index)
        {
            selected = Selected.Object;
            _activeCellObject = (HexCellObjectType)index;
        }
        
        private enum Selected
        {
            Type,
            Object
        }
    }
}
