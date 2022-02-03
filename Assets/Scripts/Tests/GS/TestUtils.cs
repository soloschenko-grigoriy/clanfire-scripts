using System.Collections.Generic;
using System.Linq;
using GS.Hex;
using GS.Units;
using NSubstitute;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tests.GS
{
    public static class TestUtils
    {
        private const string DirPrefabsUnits = "Assets/Prefabs/Units";

        public static Unit InitUnit(string prefab)
        {
            return Object.Instantiate(AssetDatabase.LoadAssetAtPath<Unit>($"{DirPrefabsUnits}/{prefab}"));
        }
        
        // TODO does work with non 100, do your math! 
        public static float GetFakeElapsed(float desiredProgression, float duration)
        {
            var multiplayer = duration / 100;
            
            return Time.time + (desiredProgression * duration) / multiplayer;
        }
        
        public static List<IHexCell> MockPathFor(IUnit unit, IHexCell currentCell)
        {
            var path = new List<IHexCell>();
            for (int i = 0; i < 6; i++)
            {
                path.Add(Substitute.For<IHexCell>());
            }
            
            unit.CurrentPath.Returns(path);
            unit.Destination.Returns(path.First());
            unit.Cell.Returns(currentCell);
            
            return path;
        }

        public static IHexCell MockCell()
        {
            return Substitute.For<IHexCell>();
        }
    }
}
