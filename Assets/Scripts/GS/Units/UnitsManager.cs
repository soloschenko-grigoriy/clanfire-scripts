using System.Collections.Generic;
using UnityEngine;

namespace GS.Units
{
    [RequireComponent(typeof(PlayerController))]
    public class UnitsManager : MonoBehaviour
    {
        [SerializeField] private int spawnAmount = 3;
        [SerializeField] private int spawnRows = 1;
        [SerializeField] private Unit unitPrefab;
        [SerializeField] private Transform spawnPoint;

        public bool IsAnySelected => SelectedUnits.Count > 0;
        
        public List<Unit> Units { get; } = new List<Unit>();
        public List<Unit> SelectedUnits { get; } = new List<Unit>();
        public PlayerController PlayerController { get; private set; }

        private void Awake()
        {
            PlayerController = GetComponent<PlayerController>();
            Spawn();
        }

        private void Spawn()
        {
            for (int x = 0, i = 0; x < spawnAmount / spawnRows; x++)
            {
                for (var z = 0; z < spawnRows; z++, i++)
                {
                    var unit = Instantiate(unitPrefab, transform, true);
                    unit.Spawn(spawnPoint.position + new Vector3(5 * x, 0, z * 5), this, i);

                    Units.Add(unit);
                }
            }
        }

        public void Select(Unit unit, bool preserveAlreadySelected)
        {
            if (!CheckIfUnitBelongsToPlayer(unit))
            {
                return;
            }
            
            if (!preserveAlreadySelected)
            {
                DeselectAll();
            }
            
            SelectedUnits.Add(unit);
            unit.SetSelected(true);
        }

        private void Deselect(Unit unit, bool preserveAlreadySelected)
        {
            if (!CheckIfUnitBelongsToPlayer(unit))
            {
                return;
            }
            
            if (!preserveAlreadySelected)
            {
                DeselectAll();
            }
            else
            {
                SelectedUnits.Remove(unit);
                unit.SetSelected(false);
            }
        }

        public void Toggle(Unit unit, bool preserveAlreadySelected)
        {
            if (unit.IsSelected)
            {
                Deselect(unit, preserveAlreadySelected);
            }
            else
            {
                Select(unit, preserveAlreadySelected);
            }
        }

        public void MoveAllSelectedTowards(Vector3 target)
        {
            foreach (var unit in SelectedUnits)
            {
                unit.MarchTowards(target);
            }
        }
        
        public void MoveAndAttack(Unit foe)
        {
            if (!IsAnySelected)
            {
                return;
            }
            
            foreach (var unit in SelectedUnits)
            {
                unit.SetTarget(foe);
            }
        }
        
        public void DeselectAll()
        {
            if (!IsAnySelected)
            {
                return;
            }

            foreach (var unit in SelectedUnits)
            {
                unit.SetSelected(false);
            }

            SelectedUnits.Clear();
        }

        public void Recycle(Unit unit)
        {
            Units.Remove(unit);
            SelectedUnits.Remove(unit);
            unit.gameObject.SetActive(false);
        }
        
        private bool CheckIfUnitBelongsToPlayer(Unit unit)
        {
            return Units.Contains(unit);
        }
    }
}
