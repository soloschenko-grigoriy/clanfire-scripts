using System;
using UnityEngine;

namespace GS.Hex
{
    public class HexGridChunk : MonoBehaviour
    {
        [SerializeField] private Color[] colors;

        public Color[] Colors => colors;
        
        private HexCell[] _cells;
        private HexMesh _mesh;

        private void Awake()
        {
            _mesh = GetComponentInChildren<HexMesh>();

            _cells = new HexCell[HexMetrics.ChunkSizeX * HexMetrics.ChunkSizeZ];
        }

        private void Start()
        {
            Refresh();
        }

        private void OnValidate()
        {
            Refresh();
        }

        public void AddCell(int index, HexCell cell)
        {
            _cells[index] = cell;
            cell.SetChunk(this);
            cell.transform.SetParent(transform, false);
        }

        public void Refresh()
        {
            if (!_mesh)
            {
                return;
            }
            _mesh.Triangulate(_cells, false);
        }
    }
}
