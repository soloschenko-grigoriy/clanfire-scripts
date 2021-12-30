using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GS.UI
{
    public class ConfirmNewTown : MonoBehaviour
    {
        [SerializeField] protected RectTransform popup;
        [SerializeField] protected RectTransform bg;
        
        [SerializeField] private TMP_InputField input;
        [SerializeField] private RectTransform errorElm;
        [SerializeField] private Button confirmBtn;
        
        private UnityAction _onConfirmAction;
        private UnityAction _onCancelAction;

        private void OnEnable()
        {
            input.onSubmit.AddListener(arg0 => OnConfirm());
        }

        private void OnDisable()
        {
            input.onSubmit.RemoveAllListeners();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                OnCancel();
            }
        }

        public void Show(UnityAction onConfirm = null, UnityAction onCancel = null)
        {
            DisableConfirm();
            
            _onConfirmAction = onConfirm;
            _onCancelAction = onCancel;

            popup.gameObject.SetActive(true);
            bg.gameObject.SetActive(true);
            
            input.text = "";
            input.ActivateInputField();
            
            Time.timeScale = 0;
        }

        public void OnConfirm()
        {
            if (input.text.Length < 3)
            {
                ShowError("City name should be at least 3 characters long");
                return;
            }
            
            _onConfirmAction?.Invoke();
            Hide();
        }

        public void OnCancel()
        {
            _onCancelAction?.Invoke();
            Hide();
        }

        public void OnValueChange()
        {
            ClearError();
            if (input.text.Length < 3)
            {
                DisableConfirm();
            }
            else
            {
                EnableConfirm();
            }
        }
        
        private void Hide()
        {
            Time.timeScale = 1;
            
            popup.gameObject.SetActive(false);
            bg.gameObject.SetActive(false);
            
            ClearError();

            _onConfirmAction = null;
            _onCancelAction = null;
        }
        
        private void ShowError(string msg)
        {
            errorElm.GetComponentInChildren<TextMeshProUGUI>().text = msg;
            errorElm.gameObject.SetActive(true);
        }
        
        private void ClearError()
        {
            errorElm.gameObject.SetActive(false);
            errorElm.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        private void EnableConfirm()
        {
            confirmBtn.interactable = true;
            confirmBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 255, 255, 1f);
        }
        
        private void DisableConfirm()
        {
            confirmBtn.interactable = false;
            confirmBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 255, 255, 0.2f);
        }
    }
}
