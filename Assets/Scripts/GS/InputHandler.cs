using GS.Players;
using GS.Players.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GS
{
    [RequireComponent(typeof(HumanControllerBehaviour))]
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private int maxHitDistance = 500;

        private const int GroundLayerMask = 1 << 6;
        
        private Camera _camera;
        private HumanControllerBehaviour _controller;

        private void Awake()
        {
            _camera = Camera.main;
            _controller = GetComponent<HumanControllerBehaviour>();
        }
        
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            OnMouseLeftClick(Input.mousePosition);
        }

        private void OnMouseLeftClick(Vector3 target)
        {
            if (!Physics.Raycast(_camera.ScreenPointToRay(target), out var hit, maxHitDistance, GroundLayerMask))
            {
                return;
            }

            _controller.HumanController.OnTouch(hit.point);
        }
    }
}
