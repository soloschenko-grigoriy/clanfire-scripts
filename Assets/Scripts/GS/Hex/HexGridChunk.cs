using System;
using UnityEngine;

namespace GS.Hex
{
    public class HexGridChunk : MonoBehaviour, IRefreshable
    {
        [SerializeField] private Color[] colors;
        [SerializeField] private Color pathBorderColor = new Color(0.75f, 0.75f, 0.75f);
        [SerializeField] private Color highlightColor = new Color(0.66f, 0.20f, 0.92f);

        public Color[] Colors => colors;
        public Color PathBorderColor => pathBorderColor;
        public Color HighlightColor => highlightColor;
        
        
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
