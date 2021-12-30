using System;
using UnityEngine;

namespace GS.Hex
{
    [Serializable]
    public struct HexCoordinates
    {
        [SerializeField] private int x;
        [SerializeField] private int z;

        public int X => x;
        public int Z => z;
        public int Y => -X - Z;
        
        public HexCoordinates(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public static HexCoordinates FromOffsetCoordinates(int x, int z) => new HexCoordinates(x - z / 2, z);

        public override string ToString() => "(" + X + ", " + Y + ", " + Z + ")";

        public override bool Equals(object obj) =>
            obj is HexCoordinates coord && X == coord.X && Y == coord.Y && Z == coord.Z;

        public override int GetHashCode() => X ^ Y ^ Z;

        public static bool operator ==(HexCoordinates lhs, HexCoordinates rhs) {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(HexCoordinates lhs, HexCoordinates rhs) {
            return !lhs.Equals(rhs);
        }
        
        public string ToStringOnSeparateLines() => X + "\n" + Y + "\n" + Z;

        public static HexCoordinates FromPosition(Vector3 position)
        {
            float x = position.x / (HexMetrics.InnerRadius * 2f);
            float y = -x;
            
            float offset = position.z / (HexMetrics.OuterRadius * 3f);
            x -= offset;
            y -= offset;
            
            int iX = Mathf.RoundToInt(x);
            int iY = Mathf.RoundToInt(y);
            int iZ = Mathf.RoundToInt(-x -y);

            if (iX + iY + iZ != 0) {
                float dX = Mathf.Abs(x - iX);
                float dY = Mathf.Abs(y - iY);
                float dZ = Mathf.Abs(-x -y - iZ);

                if (dX > dY && dX > dZ) {
                    iX = -iY - iZ;
                }
                
                else if (dZ > dY) {
                    iZ = -iX - iY;
                }
            }
            
            return new HexCoordinates(iX, iZ);
        }

        public static int DistanceBetween(HexCoordinates a, HexCoordinates b)
        {
            return ((Mathf.Abs(a.X - b.X) + Mathf.Abs(a.Y - b.Y) + Mathf.Abs(a.Z - b.Z))) / 2;
        }
    }
}
