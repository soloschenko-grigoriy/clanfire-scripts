using UnityEngine;
using TMPro;

namespace GS.UI
{
    public class StructureMenuPopup : MonoBehaviour
    {
        [SerializeField] protected RectTransform popup;
        [SerializeField] protected RectTransform bg;
        
        [SerializeField] protected TextMeshProUGUI titleElm;
        
        public void Show(string title)
        {
            titleElm.text = title;
            
            popup.gameObject.SetActive(true);
            bg.gameObject.SetActive(true);
        }

        public void OnClose() => Hide();

        private void Hide()
        {
            popup.gameObject.SetActive(false);
            bg.gameObject.SetActive(false);
        }
    }
}
