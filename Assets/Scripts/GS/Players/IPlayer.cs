using GS.Hex;
using GS.Structures;
using GS.Units;

namespace GS.Players
{
    public interface IPlayer
    {
        PlayerBehaviour PlayerBehaviour { get; }
        IHexGrid Grid { get;  }
        IUnit SelectedUnit { get; }
        bool IsCurrent { get; }

        void SpawnUnits(IUnit[] units, HexCoordinates coordinates);
        void SelectUnit(IUnit unit);
        void SetAsCurrent();
    }
}
