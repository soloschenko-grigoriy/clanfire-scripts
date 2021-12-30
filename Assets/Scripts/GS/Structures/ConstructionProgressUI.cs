using UnityEngine;
using UnityEngine.UI;

namespace GS.Structures
{
    public class ConstructionProgressUI : MonoBehaviour
    {
        [SerializeField] private Image progressImage;
        [SerializeField] private float posY;
        
        public void SetValue(float value)
        {
            progressImage.fillAmount = value;
        }

        public void MoveUp()
        {
            var pos = transform.localPosition;
            transform.localPosition = new Vector3(pos.x, posY, pos.z);
        }
    }
}
