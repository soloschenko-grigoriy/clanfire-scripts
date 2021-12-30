using System;
using System.Collections.Generic;
using UnityEngine;

namespace GS.Hex
{
    [RequireComponent(
        typeof(MeshFilter), 
        typeof(MeshRenderer), 
        typeof(MeshCollider)
        )]
    public class HexMesh : MonoBehaviour
    {
        private Mesh _mesh;
        private List<Vector3> _vertices;
        private List<int> _triangles;
        private List<Color> _colors;
        private MeshCollider _collider;
        private bool _blendColors;

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
            _collider = GetComponent<MeshCollider>();
            
            _mesh.name = "Hex Mesh";
            
            _vertices = new List<Vector3>();
            _triangles = new List<int>();
            _colors = new List<Color>();
        }

        public void Triangulate(HexCell[] cells, bool blendColors)
        {
            Cleanup();

            _blendColors = blendColors;

            foreach (var cell in cells)
            {
                Triangulate(cell);
            }

            _mesh.vertices = _vertices.ToArray();
            _mesh.colors = _colors.ToArray();
            _mesh.triangles = _triangles.ToArray();
            _mesh.RecalculateNormals();
            _collider.sharedMesh = _mesh;
        }

        private void Triangulate(HexCell cell)
        {
            for (var d = HexDirection.NE; d <= HexDirection.NW; d++)
            {
                Triangulate(d, cell);
            }
        }

        private void Triangulate(HexDirection direction, HexCell cell)
        {
            var center = cell.transform.localPosition;
            var v1 = center + HexMetrics.GetFirstSolidCorner(direction);
            var v2 = center + HexMetrics.GetSecondSolidCorner(direction);

            if (_blendColors)
            {
                AddTriangle(center, v1, v2);
            }
            else
            {
                AddTriangle(
                    center,
                    center + HexMetrics.GetFirstCorner(direction),
                    center + HexMetrics.GetSecondCorner(direction)
                );
            }
            
            AddTriangleColor(cell.Color);
            
            if(_blendColors && direction <= HexDirection.SE)
            {
                TriangulateConnection(direction, cell, v1, v2);
            }

            AddBorder(direction, cell);
            
            if(direction <= HexDirection.SE)
            {
                AddSlope(direction, cell);
            }
        }

        private void TriangulateConnection(HexDirection direction, HexCell cell, Vector3 v1, Vector3 v2)
        {
            var neighbor = cell.GetNeighbor(direction);
            if (neighbor == null)
            {
                return;
            }
            
            var bridge = HexMetrics.GetBorder(direction);
            var v3 = v1 + bridge;
            var v4 = v2 + bridge;

            v3.y = v4.y = neighbor.Elevation;
            
            AddQuad(v1, v2, v3, v4);
            AddQuadColor(Color.grey);

            var nextNeighbor = cell.GetNeighbor(direction.Next());
            if (direction <= HexDirection.E && nextNeighbor != null)
            {
                Vector3 v5 = v2 + HexMetrics.GetBridge(direction.Next());
                v5.y = nextNeighbor.Elevation;
                AddTriangle(v2, v4, v5);
                AddTriangleColor(cell.Color, neighbor.Color, nextNeighbor.Color);
            }
        }

        private void AddBorder(HexDirection direction, HexCell cell)
        {
            var center = cell.transform.localPosition;
            var v1 = center + HexMetrics.GetBorderFirstSolidCorner(direction);
            var v2 = center + HexMetrics.GetBorderSecondSolidCorner(direction);
            var bridge = HexMetrics.GetBorder(direction);
            var v3 = v1 + bridge;
            var v4 = v2 + bridge;
            
            v1.y = v2.y = v3.y = v4.y = cell.Elevation + 0.01f;
            
            AddQuad(v1, v2, v3, v4);
            AddQuadColor(Color.grey);
            AddTriangle(v2, v4, v2 + HexMetrics.GetBorder(direction.Next()));
            AddTriangleColor(Color.grey);
        }
        
        private void AddSlope(HexDirection direction, HexCell cell)
        {
            var neighbor = cell.GetNeighbor(direction);
            if (neighbor == null)
            {
                return;
            }
            
            var center = cell.transform.localPosition;
            var v1 = center + HexMetrics.GetFirstCorner(direction);
            var v2 = center + HexMetrics.GetSecondCorner(direction);
            var v3 = v1;
            var v4 = v2;
            
            v1.y = v2.y = v3.y = v4.y = cell.Elevation + 0.01f;
            
            v3.y = v4.y = neighbor.Elevation;
            
            AddQuad(v1, v2, v3, v4);
            AddQuadColor(Color.grey);
        }
        
        private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = _vertices.Count;
            _vertices.Add(v1);
            _vertices.Add(v2);
            _vertices.Add(v3);
            
            _triangles.Add(vertexIndex);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
        }

        private void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            int vertexIndex = _vertices.Count;
            _vertices.Add(v1);
            _vertices.Add(v2);
            _vertices.Add(v3);
            _vertices.Add(v4);
            
            _triangles.Add(vertexIndex);
            _triangles.Add(vertexIndex + 2);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
            _triangles.Add(vertexIndex + 3);
        }

        private void AddQuadColor(Color c1, Color c2)
        {
            _colors.Add(c1);
            _colors.Add(c1);
            _colors.Add(c2);
            _colors.Add(c2);
        }
        
        private void AddQuadColor(Color c1)
        {
            _colors.Add(c1);
            _colors.Add(c1);
            _colors.Add(c1);
            _colors.Add(c1);
        }
        
        private void AddTriangleColor(Color color)
        {
            _colors.Add(color);
            _colors.Add(color);
            _colors.Add(color);
        }

        private void AddTriangleColor(Color c1, Color c2, Color c3)
        {
            _colors.Add(c1);
            _colors.Add(c2);
            _colors.Add(c3);
        }

        private void Cleanup()
        {
            _mesh.Clear();
            _vertices.Clear();
            _triangles.Clear();
            _colors.Clear();
        }
    }
}


