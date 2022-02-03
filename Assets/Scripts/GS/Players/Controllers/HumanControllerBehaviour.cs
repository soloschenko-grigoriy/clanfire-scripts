using UnityEngine;

namespace GS.Players.Controllers
{
    [RequireComponent(typeof(PlayerBehaviour))]
    public class HumanControllerBehaviour : MonoBehaviour
    {
        // TODO this should not be here
        // [SerializeField] private ConfirmNewTown confirmPopup;
        // [SerializeField] private Structure townHallPrefab;
        
        // private IPlayer Player => _player ??= _player = GetComponent<PlayerBehaviour>().Player;
        public HumanController HumanController => _humanController;
        public PlayerBehaviour PlayerBehaviour => _playerBehaviour;
        
        private IPlayer _player;
        private PlayerBehaviour _playerBehaviour;
        private HumanController _humanController;

        private void Awake()
        {
            _playerBehaviour = GetComponent<PlayerBehaviour>();
            _humanController = new HumanController(_playerBehaviour.Player);
            _humanController.SetBehaviour(this);
        }
    }
}

// _confirmOpen = true;
// confirmPopup.Show(() => {
//     _confirmOpen = false;
//     cell.SetContents(townHallPrefab, HexCellObjectType.Building, false);
//     cell.SetCategory(HexCellCategory.Building);
//     cell.Chunk.Refresh();
// }, () => {
//     _confirmOpen = false;
// });
