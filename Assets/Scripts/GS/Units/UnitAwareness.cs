using System.Linq;
using Castle.Core.Internal;
using UnityEditor;
using UnityEngine;

namespace GS.Units
{
    public class UnitAwareness : MonoBehaviour
    {
        private Unit _unit;
        private UnitActivity _unitActivity;
        private const int LayerMask = 1 << 7;
        private const int MAXColliders = 100;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
            _unitActivity = _unit.GetComponent<UnitActivity>();
        }

        private void Update()
        {
            if (_unitActivity.TargetIsSet || _unit.IsSelected)
            {
                return;
            }
            
            CheckForTargetNearby();
        }

        private void CheckForTargetNearby()
        {
            Collider[] hitColliders = new Collider[MAXColliders];
            Physics.OverlapSphereNonAlloc(transform.position, _unit.Config.Awareness, hitColliders, LayerMask);
            var foes = hitColliders
                .FindAll(collider => collider && collider.GetComponent<Unit>().GetRelationshipTo(_unit) == PlayerRelationship.Foe)
                .OrderBy(collider => (collider.transform.position - transform.position).magnitude)
                .ToArray();

            if (foes.Length > 0)
            {
                _unitActivity.SetTarget(foes[0].GetComponent<Unit>(), true);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.up, _unit.Config.Awareness);
            
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, transform.up, _unit.Config.TargetGiveUpDistance);
        }
#endif
    }
}
