using System;
using UnityEngine;

namespace GS.Players.Controllers
{
    [Serializable]
    public class HumanController : IHumanController
    {
        public IPlayer Player => _player ??= _player = _humanBehaviour.PlayerBehaviour.Player;
        
        private bool _menuIsOpen;
        private IPlayer _player;
        private HumanControllerBehaviour _humanBehaviour;

        public HumanController(IPlayer player)
        {
            _player = player;
        }
        
        public void SetBehaviour(HumanControllerBehaviour behaviour)
        {
            _humanBehaviour = behaviour;
        }
        
        public void OnTouch(Vector3 point)
        {
            if (_menuIsOpen)
            {
                return;
            }

            // freeze all motor functions
            if (!Player.IsCurrent)
            {
                return;
            }
            
            var cell = Player.Grid.GetCell(point);
            if (cell == null)
            {
                return;
            }
            
            if (cell.Unit != null)
            {
                Player.SelectUnit(cell.Unit);
                return;
            }

            // if (cell.Category == HexCellCategory.Building && cell.HasContents)
            // {
            //     _player.SelectStructure(cell.Contents.GetComponent<Structure>());
            //     return;
            // }

            if (Player.SelectedUnit == null)
            {
                return;
            }

            if (Player.SelectedUnit.Destination == cell)
            {
                Player.SelectedUnit.StartMovingTowardsDestination();
                return;
            }

            Player.SelectedUnit.TrySetDestination(cell);
        }

        public void OnMenuOpen()
        {
            _menuIsOpen = true;
        }

        public void OnMenuClosed()
        {
            _menuIsOpen = false;
        }
    }
}
