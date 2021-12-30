using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GS.UI
{
    public class UnderConstructionPopup : MonoBehaviour
    {
        [SerializeField] private RectTransform popup;
        [SerializeField] protected RectTransform bg;
        
        [SerializeField] protected TextMeshProUGUI titleElm;
        [SerializeField] private Slider progressUI;
        [SerializeField] private ConfirmPopup confirm;
    
        private UnityAction _onStopAction;
        private UnityAction _onCloseAction;
        
        public void Show(string title, UnityAction onStop, UnityAction onClose)
        {
            titleElm.text = title;
            _onStopAction = onStop;
            _onCloseAction = onClose;
            
            Show();
        }
        
        public void SetValue(float value)
        {
            progressUI.value = value;
        }

        public void OnStop()
        {
            Hide();
            confirm.Show("You sure you want to stop the construction? All progress will be lost!", _onStopAction, OnClose);
        }

        public void OnClose() 
        {
            Hide();
            _onCloseAction();
        }

        private void Show()
        {
            popup.gameObject.SetActive(true);
            bg.gameObject.SetActive(true);
        }

        private void Hide()
        {
            popup.gameObject.SetActive(false);
            bg.gameObject.SetActive(false);
        }
    }
}
