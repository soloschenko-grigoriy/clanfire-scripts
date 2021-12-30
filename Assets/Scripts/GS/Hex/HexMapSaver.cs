using System;
using System.Linq;
using GS.Helpers;
using UnityEngine;

namespace GS.Hex
{
    public class HexMapSaver: MonoBehaviour
    {
        [SerializeField] private HexGrid grid;
        
        private SaveLoadLocalFile _saverLoader;

        private void Awake()
        {
            _saverLoader = new SaveLoadLocalFile("maps.json");
        }

        public void Save()
        {
            var id = 1;
            var mapName = "Hello";
            var cells = grid.Cells
                .Select(cell => new HexCellData(cell.Category, cell.Coordinates, cell.ContentsType))
                .ToArray();
            
            _saverLoader.Erase();
            _saverLoader.Save(new HexMapData(id, mapName, cells));
        }

        public void LoadMap()
        {
            var data = _saverLoader.Load<HexMapData>();
            Array.ForEach(grid.Cells, cell => cell.Cleanup());
            Array.ForEach(data.c, item => grid.ChangeCell(item.c, item.ca, item.co));
            grid.Refresh();
        }
    }
}
