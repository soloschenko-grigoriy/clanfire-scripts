using GS.Units;
using UnityEngine;

namespace GS
{
    [RequireComponent(typeof(UnitsManager))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Texture2D defaultCursor;
        [SerializeField] private Texture2D moveCursor;
        [SerializeField] private Texture2D unAccessibleCursor;
        [SerializeField] private Texture2D offenseCursor;
        [SerializeField] private PointFx pointFx;

        private Unit _unitHovered;
        private bool _anyUnAccessibleHovered;

        public UnitsManager UnitsManager { get; private set; }

        private void Awake()
        {
            UnitsManager = GetComponent<UnitsManager>();
            SetCursor();
        }

        private void Update()
        {
            SetCursor();
        }

        public void UnitClicked(RaycastHit hit, bool preserveAlreadySelected)
        {
            var target = hit.collider.GetComponent<Unit>();
            if (target.GetRelationshipTo(this) == PlayerRelationship.Foe)
            {
                pointFx.Stop();
                UnitsManager.MoveAndAttack(target);
            }
            else
            {
                UnitsManager.Toggle(target, preserveAlreadySelected);
            }
        }

        public void GroundClicked(RaycastHit hit)
        {
            if (!UnitsManager.IsAnySelected)
            {
                return;
            }

            pointFx.ShowAt(hit.point);
            UnitsManager.MoveAllSelectedTowards(hit.point);
            // , pointFx.Stop
        }

        public void UnknownClicked(RaycastHit hit)
        {
            UnitsManager.DeselectAll();
        }

        public void CancelAllSelections()
        {
            UnitsManager.DeselectAll();
        }

        private void SetCursor()
        {
            var texture = defaultCursor;
            if (_anyUnAccessibleHovered)
            {
                texture = unAccessibleCursor;
            } 
            else if (UnitsManager.IsAnySelected && _unitHovered == null)
            {
                texture = moveCursor;
            }
            else if(UnitsManager.IsAnySelected && _unitHovered != null && _unitHovered.GetRelationshipTo(this) == PlayerRelationship.Foe)
            {
                texture = offenseCursor;
            }

            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        }

        public void UnitHovered(RaycastHit hit)
        {
            _unitHovered = hit.collider.GetComponent<Unit>();
            _anyUnAccessibleHovered = false;
        }
        
        public void UnitUnHovered()
        {
            _unitHovered = null;
        }

        public void UnAccessibleHovered(RaycastHit hitInfo)
        {
            _unitHovered = null;
            _anyUnAccessibleHovered = true;
        }

        public void UnAccessibleUnHovered()
        {
            _anyUnAccessibleHovered = false;
        }
    }
}
