using System;
using System.Collections.Generic;
using GS.Hex;
using GS.Structures;
using GS.Units;
using UnityEngine;

namespace GS.Players
{
    [Serializable]
    public class Player: IPlayer
    {
        public IUnit SelectedUnit => _selectedUnit ??= _selectedUnitGO?.GetComponent<IUnit>();
        public IHexGrid Grid => _grid ??= _gridGO.GetComponent<IHexGrid>();
        public bool IsCurrent => _isCurrent;
        public PlayerBehaviour PlayerBehaviour => _playerBehaviour;


        // keep this coz unity looses track of interfaces after domain reload
        private GameObject _selectedUnitGO; 
        private IUnit _selectedUnit;
        private List<IUnit> _units = new List<IUnit>();
        private IHexGrid _grid;
        private GameObject _gridGO;
        private PlayerBehaviour _playerBehaviour;
        private bool _isCurrent;

        public Player(IHexGrid grid)
        {
            _gridGO = grid.GameObject;
            _grid = grid;
        }

        public void SpawnUnits(IUnit[] units, HexCoordinates coordinates)
        {
            var cell = Grid.GetCell(coordinates);
            
            Array.ForEach(units, unit => {
                unit.Spawn(cell, this);
                _units.Add(unit);
            });
        }

        public void SelectUnit(IUnit unit)
        {
            if (_selectedUnitGO)
            {
                SelectedUnit.DeSelect();
            }
            
            _selectedUnit = unit;
            _selectedUnitGO = unit.GameObject;
            
            _selectedUnit.Select();
        }

        public void SetAsCurrent()
        {
            _isCurrent = true;
        }
    }
}
