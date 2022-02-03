using UnityEngine;

namespace GS.Players.Controllers
{
    [RequireComponent(typeof(PlayerBehaviour))]
    public class AIPlayerController : MonoBehaviour
    {
        private IPlayer _player;

        private void Awake()
        {
            _player = GetComponent<IPlayer>();
        }
    }
}
