using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GS
{
    public class PointFx : MonoBehaviour
    {
        [SerializeField] private float waitBeforeHide = 2f;
        
        public async void ShowAt(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
            
            await Task.Delay(TimeSpan.FromSeconds(waitBeforeHide));
            Stop();
        }

        public void Stop()
        {
            gameObject.SetActive(false);
        }
    }
}
