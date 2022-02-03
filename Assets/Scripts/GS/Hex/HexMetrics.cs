using UnityEngine;

namespace GS.Hex
{
    public static class HexMetrics
    {
        public const int ChunkSizeX = 5;
        public const int ChunkSizeZ = 5;
        public const float OuterRadius = 5f;
        public const float InnerRadius = OuterRadius * 0.866025404f;
        
        private const float SolidFactor = 0.75f;
        private const float BlendFactor = 1 - SolidFactor;
        
        private const float BorderWidth = 0.05f;
        private const float BorderSolidFactorWidth = 1 - BorderWidth;
        
        private static readonly Vector3[] CORNERS = {
            new Vector3(0f, 0f, OuterRadius),
            new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
            new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
            new Vector3(0f, 0f, -OuterRadius),
            new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
            new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius),
            new Vector3(0f, 0f, OuterRadius)
        };

        public static Vector3 GetFirstCorner(HexDirection direction) => CORNERS[(int)direction];

        public static Vector3 GetSecondCorner(HexDirection direction) => CORNERS[(int)direction + 1];
        public static Vector3 GetFirstSolidCorner(HexDirection direction) => GetFirstCorner(direction) * SolidFactor;
        public static Vector3 GetSecondSolidCorner(HexDirection direction) => GetSecondCorner(direction) * SolidFactor;
        public static Vector3 GetBridge(HexDirection direction) => (GetFirstCorner(direction) + GetSecondCorner(direction)) * BlendFactor;
        
        public static Vector3 GetBorderFirstSolidCorner(HexDirection direction) => GetFirstCorner(direction) * BorderSolidFactorWidth;
        public static Vector3 GetBorderSecondSolidCorner(HexDirection direction) => GetSecondCorner(direction) * BorderSolidFactorWidth;
        public static Vector3 GetBorder(HexDirection direction) => (GetFirstCorner(direction) + GetSecondCorner(direction)) * BorderWidth;
    }
}
