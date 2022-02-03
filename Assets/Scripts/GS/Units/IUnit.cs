using System.Collections.Generic;
using GS.Hex;
using GS.Players;

namespace GS.Units
{
    public interface IUnit: IHasPosition, IIsGameObject
    {
        IHexCell Destination { get; }
        List<IHexCell> CurrentPath { get; }
        IHexCell Cell { get; }

        void Spawn(HexCell on, IPlayer player);
        void SetCell(IHexCell cell);
        void ResetDestination();
        void Select();
        void DeSelect();
        void TrySetDestination(IHexCell cell);
        void StartMovingTowardsDestination();
    }
}
