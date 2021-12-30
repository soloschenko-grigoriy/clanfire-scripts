using Castle.Core.Internal;
using UnityEngine;
using UnityEngine.UI;

namespace GS.Hex
{
    public class HexGrid : MonoBehaviour
    {
        [SerializeField] private int chunkCountX = 4;
        [SerializeField] private int chunkCountZ = 4;
        [SerializeField] private HexCell cellPrefab;
        [SerializeField] private Text hexCellLabelPrefab;
        [SerializeField] private HexGridChunk chunkPrefab;
        [SerializeField] private bool showLabels;
        [SerializeField] private HexCellObject[] objectPrefabs;
        
        public HexCell[] Cells => _cells;
        public HexGridChunk[] Chunks => _chunks;

        private HexCell[] _cells;
        private HexGridChunk[] _chunks;
        private Canvas _canvas;
        private int _cellCountX = 6;
        private int _cellCountZ = 6;

        private void Awake ()
        {
            _canvas = GetComponentInChildren<Canvas>();

            _cellCountX = chunkCountX * HexMetrics.ChunkSizeX;
            _cellCountZ = chunkCountZ * HexMetrics.ChunkSizeZ;

            CreateChunks();
            CreateCells();
        }

        public void ChangeCell(Vector3 point, HexCellCategory category)
        {
            var cell = GetCell(point);
            ChangeCell(cell, category);
            cell.Chunk.Refresh();
        }
        
        public void ChangeCell(Vector3 point, HexCellObjectType type)
        {
            var cell = GetCell(point);
            ChangeCell(cell, type);
            cell.Chunk.Refresh();
        }

        public void ChangeCell(HexCell cell, HexCellCategory category)
        {
            if (cell.HasContents)
            {
                Debug.LogWarning("Cannot change type of the cell that has contents");
                return;
            }
            
            cell.SetCategory(category);
            cell.SetElevation(category == HexCellCategory.DeepWater || category == HexCellCategory.ShallowWater ? -1 : 0);
        }
        
        public void ChangeCell(HexCell cell, HexCellObjectType type)
        {
            var category = cell.Category;
            if (type == HexCellObjectType.Building)
            {
                category = HexCellCategory.Building;
            } 
            else if (type == HexCellObjectType.Forest)
            {
                category = HexCellCategory.Grass;
            }
            else if (type == HexCellObjectType.Rock || type == HexCellObjectType.Moutain)
            {
                category = HexCellCategory.Rock;
            }

            cell.SetCategory(category);
            cell.SetElevation(category == HexCellCategory.DeepWater || category == HexCellCategory.ShallowWater ? -1 : 0);
            cell.SetContents(type > 0 ? objectPrefabs[(int) type  - 1] : null, type);
        }

        public void ChangeCell(HexCoordinates coordinates, HexCellCategory category, HexCellObjectType type)
        {
            var cell = _cells.Find(cell => cell.Coordinates == coordinates);
            
            ChangeCell(cell, category);
            ChangeCell(cell, type);
        }
        
        public HexCell GetRandomCell() => _cells[Random.Range(0, _cells.Length - 1)];

        public void Refresh()
        {
            foreach (var chunk in _chunks)
            {
                chunk.Refresh();
            }
        }
        
        public HexCell GetCell(Vector3 point)
        {
            var pos = transform.InverseTransformPoint(point);
            var coordinates = HexCoordinates.FromPosition(pos);
            var index = GetIndex(coordinates);
            
            return _cells[index];
        }
        
        private void CreateChunks()
        {
            _chunks = new HexGridChunk[chunkCountX * chunkCountZ];

            for (int z = 0, i = 0; z < chunkCountZ; z++)
            {
                for (int x = 0; x < chunkCountX; x++, i++)
                {
                    _chunks[i] = Instantiate(chunkPrefab, transform, true);
                }
            }
        }
        
        private void CreateCells()
        {
            _cells = new HexCell[_cellCountZ * _cellCountX];

            for (int z = 0, i = 0; z < _cellCountZ; z++) {
                for (int x = 0; x < _cellCountX; x++) {
                    CreateCell(x, z, i++);
                }
            }
            
            // A way to set all cell static so it will produce less batches
            StaticBatchingUtility.Combine(this.gameObject);
        }
        
        private void CreateCell (int x, int z, int i) {
            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (HexMetrics.InnerRadius * 2f);
            position.y = 0f;
            position.z = z * (HexMetrics.OuterRadius * 1.5f);

            HexCell cell = Instantiate(cellPrefab);
            _cells[i] = cell;
            
            cell.transform.localPosition = position;
            cell.SetCoordinates(HexCoordinates.FromOffsetCoordinates(x, z));
            cell.SetCategory(HexCellCategory.DeepWater);
            cell.SetElevation(-1);
            
            // Setup neighbors
            if (x > 0)
            {
                cell.SetNeighbor(HexDirection.W, _cells[i - 1]);
            }

            if (z > 0)
            {
                if ((z & 1) == 0) // even rows
                {
                    cell.SetNeighbor(HexDirection.SE, _cells[i - _cellCountX]);
                    if (x > 0)
                    {
                        cell.SetNeighbor(HexDirection.SW, _cells[i - _cellCountX - 1]);
                    }
                }
                else
                {
                    cell.SetNeighbor(HexDirection.SW, _cells[i - _cellCountX]);
                    if (x < _cellCountX - 1)
                    {
                        cell.SetNeighbor(HexDirection.SE, _cells[i - _cellCountX + 1]);
                    }
                }
            }

            if (showLabels)
            {
                Text label = Instantiate<Text>(hexCellLabelPrefab);
                label.rectTransform.SetParent(_canvas.transform, false);
                label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
                label.text = cell.Coordinates.ToStringOnSeparateLines();
            }

            AddCellToChunk(x, z, cell);
            
        }

        private void AddCellToChunk(int x, int z, HexCell cell)
        {
            int chunkX = x / HexMetrics.ChunkSizeX;
            int chunkZ = z / HexMetrics.ChunkSizeZ;

            var chunk = _chunks[chunkX + chunkZ * chunkCountX];
            
            int localX = x - chunkX * HexMetrics.ChunkSizeX;
            int localZ = z - chunkZ * HexMetrics.ChunkSizeZ;
            
            chunk.AddCell(localX + localZ * HexMetrics.ChunkSizeX, cell);
        }

        private int GetIndex(HexCoordinates coordinates) => coordinates.X + coordinates.Z * _cellCountX + coordinates.Z / 2;
    }
}
