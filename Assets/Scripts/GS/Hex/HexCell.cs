using GS.Structures;
using UnityEngine;

namespace GS.Hex
{
    public class HexCell : MonoBehaviour
    {
        [SerializeField] private HexCoordinates coordinates;
        [SerializeField] private HexCellCategory category = HexCellCategory.Grass;
        [SerializeField] private HexCell[] neighbors = new HexCell[6];
        [SerializeField] private int elevation = 0;
        
        public HexCoordinates Coordinates => coordinates;
        public Color Color => _chunk.Colors[(int)category];
        public HexGridChunk Chunk => _chunk;
        public int Elevation => elevation;
        public HexCellCategory Category => category;
        public HexCellObjectType ContentsType => _contentsType;
        public bool HasContents => _contents != null;
        public HexCell[] Neighbors => neighbors;
        public HexCellObject Contents => _contents;

        private HexGridChunk _chunk;
        private HexCellObject _contents;
        private HexCellObjectType _contentsType = HexCellObjectType.Empty;

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

            return true;
        }

        public void SetNeighbor(HexDirection direction, HexCell cell)
        {
            neighbors[(int)direction] = cell;
            cell.neighbors[(int)direction.Opposite()] = this;
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

        public int GetCost(HexCell to)
        {
            return 1;
        }

        public void Cleanup()
        {
            SetCategory(HexCellCategory.DeepWater);
            SetElevation(-1);
            RemoveContents();
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

    }
}
