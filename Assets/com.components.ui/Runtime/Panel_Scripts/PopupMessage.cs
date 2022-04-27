using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupMessage : SingletonBehaviour<PopupMessage>
{
    public Text messageText;
    public Text titleText;
    public GameObject messagePanel;

    public UnityEvent onClose;

    private bool _isAuto;
    private bool _allowCloseOnEnter;

    public void ShowAuto(string message,string titleString="")
    {
        _isAuto = true;
        Show(message,titleString);
    }

    public void Show(string message, string titleString = "", bool allowCloseOnEnter = true)
    {
        _allowCloseOnEnter = allowCloseOnEnter;
        messageText.text = message;
        titleText.text = titleString;
        messagePanel.SetActive(true);
    }

    private void Update()
    {
        if (!_allowCloseOnEnter) { return; }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Hide();
        }
    }

    public void HideAuto()
    {
        _isAuto = false;
        Hide();
    }

    public void Hide()
    {
        if (_isAuto) return;
        messagePanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
    }
}
