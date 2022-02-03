using System;
using GS.Hex;
using UnityEditor;
using NUnit.Framework;
using Object = UnityEngine.Object;


namespace Tests.GS.Hex
{
    public class HexCellTest
    {
        private const string HexCellPrefab = "Assets/Prefabs/Hex/Hex Cell.prefab";

        private HexCell[] _cells;

        [SetUp]
        public void SetUp()
        {
            InstantiateHexCells(10, 10);
        }
        
        [TearDown]
        public void TearDown()
        {
            Array.ForEach(_cells, Object.DestroyImmediate);
        }
        
        [Test]
        public void CanDetectAllAccessibleNearby()
        {
            Assert.That( _cells[52].GetAccessibleIn(1).Count, Is.EqualTo(7));
        }
        
        [Test]
        public void CanDetectAllAccessibleInNextCircleToo()
        {
            Assert.That(_cells[52].GetAccessibleIn(2).Count, Is.EqualTo(19));
        }
        
        [Test]
        public void WhileDetectingAccessibleShouldSkipProhibited()
        {
            _cells[52].Neighbors[0].SetCategory(HexCellCategory.DeepWater);
            Assert.That(_cells[52].GetAccessibleIn(2).Count, Is.EqualTo(17));
        }

        // TODO use actual grid instead
        private void InstantiateHexCells(int countX, int countZ)
        {
            _cells = new HexCell[countX * countZ];

            for (int z = 0, i = 0; z < countZ; z++) {
                for (int x = 0; x < countX; x++) {
                    _cells[i] = CreateCell(x, z, i++, countX);
                }
            }
        }

        private HexCell CreateCell(int x, int z, int i, int countX)
        {
            var cell = Object.Instantiate(AssetDatabase.LoadAssetAtPath<HexCell>(HexCellPrefab));

            if (x > 0)
            {
                cell.SetNeighbor(HexDirection.W, _cells[i - 1]);
            }

            if (z > 0)
            {
                if ((z & 1) == 0) // even rows
                {
                    cell.SetNeighbor(HexDirection.SE, _cells[i - countX]);
                    if (x > 0)
                    {
                        cell.SetNeighbor(HexDirection.SW, _cells[i - countX - 1]);
                    }
                }
                else
                {
                    cell.SetNeighbor(HexDirection.SW, _cells[i - countX]);
                    if (x < countX - 1)
                    {
                        cell.SetNeighbor(HexDirection.SE, _cells[i - countX + 1]);
                    }
                }
            }

            return cell;
        }
    }
}
