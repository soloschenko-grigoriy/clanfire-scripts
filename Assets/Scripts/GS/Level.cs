using GS.Hex;
using GS.Players;
using UnityEngine;

namespace GS
{
    [RequireComponent(typeof(HexMapSaver))]
    public class Level : MonoBehaviour
    {
        private PlayerBehaviour[] _players;
        private PlayerBehaviour _currentPlayerBehaviour;
        
        private void Awake()
        {
            GetComponent<HexMapSaver>().LoadMap();
        }

        private void Start()
        {
            _players = GetComponentsInChildren<PlayerBehaviour>();
            _players[0].Player.SetAsCurrent();
        }
    }
}
