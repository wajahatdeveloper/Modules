using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupMessage : SingletonBehaviourUI<PopupMessage>
{
    public Image signImage;
    public Text messageText;
    public Text titleText;
    public GameObject messagePanel;
    public Button okButton;

    public Sprite infoSignSprite;
    public Sprite warningSignSprite;
    public Sprite errorSignSprite;

	public UnityEvent onClose;

    private bool _isAuto;
    private bool _allowCloseOnEnter;
    private bool _allowBackgroundClick;

    public enum PopupSign
    {
        NONE,
        INFO,
        WARNING,
        ERROR,
    }
    
    public void ShowOnce(string message, string id, string titleString = "",PopupSign sign = PopupSign.NONE , bool allowCloseOnEnter = true
        , bool allowBackgroundClick = false)
    {
        if (PlayerPrefs.GetInt("showOnce_"+id,0) == 0)
        {
            Show(message,titleString,sign,allowCloseOnEnter, allowBackgroundClick);
            PlayerPrefs.SetInt("showOnce_"+id,1);
        }
    }
    
    public void ShowAuto(string message,string titleString="",PopupSign sign = PopupSign.NONE)
    {
        _isAuto = true;
        okButton.gameObject.SetActive(false);
        Show(message,titleString,sign,false, false);
    }

    public void Show(string message, string titleString = "",PopupSign sign = PopupSign.NONE , bool allowCloseOnEnter = true
        , bool allowBackgroundClick = false)
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

        _allowBackgroundClick = allowBackgroundClick;
        _allowCloseOnEnter = allowCloseOnEnter;
        messageText.text = message;
        titleText.text = titleString;
        messagePanel.SetActive(true);
        
        Debug.Log($"Scene: {SceneManager.GetActiveScene().name}: <b>Popup Panel</b> : Shown");
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
        okButton.gameObject.SetActive(true);
        Hide();
    }

    public void OnClick_Background()
    {
        if (_allowBackgroundClick)
        {
            Hide();
        }
    }
    
    public void Hide()
    {
        if (_isAuto) return;
        messagePanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
        
        Debug.Log($"Scene: {SceneManager.GetActiveScene().name}: <b>Popup Panel</b> : Hidden");
    }
}
