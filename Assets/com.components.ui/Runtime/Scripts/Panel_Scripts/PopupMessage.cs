using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupMessage : SingletonBehaviourUI<PopupMessage>
{
    public Image signImage;
    public Text messageText;
    public Text titleText;
    public GameObject messagePanel;

    public Sprite infoSignSprite;
    public Sprite warningSignSprite;
    public Sprite errorSignSprite;

	public UnityEvent onClose;

    private bool _isAuto;
    private bool _allowCloseOnEnter;

    public enum PopupSign
    {
        NONE,
        INFO,
        WARNING,
        ERROR,
    }
    
    public void ShowOnce(string message, string id, string titleString = "",PopupSign sign = PopupSign.NONE , bool allowCloseOnEnter = true)
    {
        if (PlayerPrefs.GetInt("showOnce_"+id,0) == 0)
        {
            Show(message,titleString,sign,allowCloseOnEnter);
            PlayerPrefs.SetInt("showOnce_"+id,1);
        }
    }
    
    public void ShowAuto(string message,string titleString="",PopupSign sign = PopupSign.NONE)
    {
        _isAuto = true;
        Show(message,titleString,sign,false);
    }

    public void Show(string message, string titleString = "",PopupSign sign = PopupSign.NONE , bool allowCloseOnEnter = true)
    {
        switch (sign)
        {
            case PopupSign.NONE:
                signImage.sprite = null;
                signImage.gameObject.SetActive(false);
                break;
            case PopupSign.WARNING:
                signImage.sprite = warningSignSprite;
                signImage.gameObject.SetActive(true);
                break;
            case PopupSign.INFO:
                signImage.sprite = infoSignSprite;
                signImage.gameObject.SetActive(true);
                break;
			case PopupSign.ERROR:
				signImage.sprite = errorSignSprite;
				signImage.gameObject.SetActive(true);
				break;
			default:
                throw new ArgumentOutOfRangeException(nameof(sign), sign, null);
        }
        
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
