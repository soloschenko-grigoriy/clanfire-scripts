using System.Collections.Generic;
using GS.Helpers;
using GS.Structures;
using GS.Units;
using UnityEngine;

namespace GS.Hex
{
    public class HexCell : MonoBehaviour, IHexCell
    {
        [SerializeField] private HexCoordinates coordinates;
        [SerializeField] private HexCellCategory category = HexCellCategory.Grass;
        [SerializeField] private HexCell[] neighbors = new HexCell[6];
        [SerializeField] private int elevation = 0;
        
        public HexCoordinates Coordinates => coordinates;
        public Color Color
        {
            get {
                if (IsHighlightedAsDestination)
                {
                    return new Color(0.66f, 0.20f, 0.92f);
                }
                
                if(IsOnPath)
                {
                    return _chunk.PathBorderColor;
                }
                
                return _chunk.Colors[(int)category];
            }
        }
        public Color HighlightColor => _chunk.HighlightColor;

        public int Elevation => elevation;
        public HexCellCategory Category => category;
        public HexCellObjectType ContentsType => _contentsType;
        public bool HasContents => _contents != null;
        public IHexCell[] Neighbors => neighbors;
        public HexCellObject Contents => _contents;
        public Unit Unit => _unit;
        public bool IsHighlightedAsAccessible { get; set; }
        public bool IsOnPath { get; set; }
        public bool IsHighlightedAsDestination { get; set; }
        public bool HasObstacle => Unit != null;
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public int Distance { get; set; } = int.MaxValue;

        private HexGridChunk _chunk;
        private HexCellObject _contents;
        private Unit _unit;
        private HexCellObjectType _contentsType = HexCellObjectType.Empty;

        private void Awake()
        {
            ResetDistance();
        }

        public HexCell GetNeighbor(HexDirection direction) => neighbors[(int)direction];

        public bool CanContainBuildings()
        {
            if (category == HexCellCategory.Building && HasContents)
            {
                return false;
            }
            
            if (category == HexCellCategory.Mountain)
            {
                return false;
            }
            
            if (category == HexCellCategory.DeepWater || category == HexCellCategory.ShallowWater)
            {
                return false;
            }

            if (_unit != null)
            {
                return false;
            }

            return true;
        }

        public void SetNeighbor(HexDirection direction, HexCell cell)
        {
            neighbors[(int)direction] = cell;
            cell.Neighbors[(int)direction.Opposite()] = this;
        }

        public void SetCoordinates(HexCoordinates coords)
        {
            coordinates = coords;
        }

        public void SetChunk(HexGridChunk chunk)
        {
            _chunk = chunk;
        }

        public void SetCategory(HexCellCategory c)
        {
            category = c;
        }

        public void SetElevation(int value)
        {
            elevation = value;

            Vector3 pos = transform.position;
            pos.y = value * 1;
            transform.localPosition = pos;
        }

        public void SetContents(HexCellObject prefab, HexCellObjectType type, bool immediateBuild = true)
        {
            if (_contents != null)
            {
               Destroy(_contents.gameObject);
               _contents = null;
            }

            if (prefab != null)
            {
                _contents = Instantiate(prefab, transform, false);
                var structure = _contents.GetComponent<Structure>();
                if (structure != null)
                {
                    structure.Build(this, immediateBuild);
                }
                
            }
            
            _contentsType = type;
        }

        public int GetCost(IHexCell to)
        {
            return 1;
        }

        public void Cleanup()
        {
            SetCategory(HexCellCategory.DeepWater);
            SetElevation(-1);
            RemoveContents();
            RemoveUnit();
        }

        public void RemoveContents()
        {
            _contentsType = HexCellObjectType.Empty;
            
            if (_contents == null)
            {
                return;
            }

            Destroy(_contents.gameObject);
            _contents = null;
        }

        public void SetUnit(Unit unit)
        {
            _unit = unit;
        }

        public void RemoveUnit()
        {
            _unit = null;
        }

        public List<IHexCell> GetAccessibleIn(int radius)
        {
            var frontier = new PriorityQueue<IHexCell>();
            frontier.Enqueue(this, 0);
            Distance = 0;
            
            var result = new List<IHexCell>();
            
            while (frontier.Count > 0) {
                var current = frontier.Dequeue();
                result.Add(current as HexCell);

                if (current.Distance >= radius)
                {
                    continue;
                }
                
                foreach (var neighbor in current.Neighbors)
                {
                    if (neighbor?.Distance != int.MaxValue || !neighbor.CanBeAccessed())
                    {
                        continue;
                    }
                    
                    neighbor.Distance = current.Distance + 1;
                    frontier.Enqueue(neighbor as HexCell, 0);
                }
            }

            return result;
        }

        public bool CanBeAccessed()
        {
            return category != HexCellCategory.Mountain && category != HexCellCategory.DeepWater;
        }

        public void ResetDistance()
        {
            Distance = int.MaxValue;
        }

        public void Refresh()
        {
            _chunk.Refresh();
        }
    }
}
