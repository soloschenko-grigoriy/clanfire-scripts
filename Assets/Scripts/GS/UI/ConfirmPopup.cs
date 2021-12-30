using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace GS.UI
{
    public class ConfirmPopup : MonoBehaviour
    {
        [SerializeField] protected RectTransform popup;
        [SerializeField] protected RectTransform bg;
        
        [SerializeField] protected TextMeshProUGUI titleElm;
        [SerializeField] protected TextMeshProUGUI textElm;

        private UnityAction _onConfirmAction;
        private UnityAction _onCancelAction;

        public void Show(string text,  UnityAction onConfirm = null, UnityAction onCancel = null, string title = "Attention!")
        {
            textElm.text = text;
            titleElm.text = title;

            _onConfirmAction = onConfirm;
            _onCancelAction = onCancel;
            
            popup.gameObject.SetActive(true);
            bg.gameObject.SetActive(true);
        }

        private void Hide()
        {
            popup.gameObject.SetActive(false);
            bg.gameObject.SetActive(false);

            _onConfirmAction = null;
            _onCancelAction = null;
        }

        public void OnConfirm()
        {
            _onConfirmAction?.Invoke();
            Hide();
        }

        public void OnCancel()
        {
            _onCancelAction?.Invoke();
            Hide();
        }
    }
}
