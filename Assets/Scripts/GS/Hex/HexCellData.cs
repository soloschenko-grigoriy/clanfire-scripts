using System;

namespace GS.Hex
{
    [Serializable] 
    public struct HexCellData
    {
        public HexCoordinates c;
        public HexCellCategory ca;
        public HexCellObjectType co;

        public HexCellData(HexCellCategory category, HexCoordinates coordinates, HexCellObjectType contentsType)
        {
            this.c = coordinates;
            this.ca = category;
            this.co = contentsType;
        }
    }
}
