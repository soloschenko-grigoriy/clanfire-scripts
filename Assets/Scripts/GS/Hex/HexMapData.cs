using System;

namespace GS.Hex
{
    [Serializable]
    public struct HexMapData
    {
        public int id;
        public string n;

        public HexCellData[] c;

        public HexMapData(int id, string name, HexCellData[] cells)
        {
            this.id = id;
            this.n = name;
            this.c = cells;
        }
    }
}
