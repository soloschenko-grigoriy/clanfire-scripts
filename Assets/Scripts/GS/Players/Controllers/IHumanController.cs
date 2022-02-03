using UnityEngine;

namespace GS.Players.Controllers
{
    public interface IHumanController
    {
        void OnTouch(Vector3 point);
        void OnMenuOpen();
        void OnMenuClosed();
    }
}
