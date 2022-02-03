using System;
using GS.Hex;
using GS.Units;
using UnityEngine;

namespace GS.Players
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private Unit settlerPrefab;
        [SerializeField] private HexGrid grid;
        [SerializeField] private bool spawnUnits;

        public IPlayer Player => _player;
        
        private Player _player;

        private void Awake()
        {
            _player = new Player(grid);
        }

        private void Start()
        {
            if (!spawnUnits)
            {
                return;
            }

            _player.SpawnUnits(new []{Instantiate(settlerPrefab, transform)}, new HexCoordinates(19, 22));
        }
    }
}
