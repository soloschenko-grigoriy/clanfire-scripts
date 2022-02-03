using System.Collections.Generic;
using GS.Units;

namespace GS.Hex
{
    public interface IHexCell: IHasPosition, IRefreshable
    {
        IHexCell[] Neighbors { get; }
        bool HasObstacle { get;  }
        bool IsOnPath { get; set; }
        bool IsHighlightedAsDestination { get; set; }
        bool IsHighlightedAsAccessible { get; set; }
        int Distance { get; set; }
        HexCellCategory Category { get; }
        int Elevation { get; }
        void SetUnit(Unit unit);
        HexCoordinates Coordinates { get; }
        bool HasContents { get; }
        Unit Unit { get; }
        
        bool CanBeAccessed();
        int GetCost(IHexCell node);
        void SetElevation(int value);
        void SetCategory(HexCellCategory c);
        void SetContents(HexCellObject prefab, HexCellObjectType type, bool immediateBuild = true);
        void ResetDistance();
    }
}
