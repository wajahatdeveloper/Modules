using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialog : SingletonBehaviourUI<Dialog>
{
    public GameObject panel;
    
    public Text message;
    public Text title;

    public Button yesButton, noButton, okButton, cancelButton;

    public Action OnYes;
    public Action OnNo;
    public Action OnOk;
    public Action OnCancel;

    public void Show(string text, string titleText="", Action onYes=null, Action onNo=null, Action onOk=null, Action onCancel=null)
    {
        SetActionAndButton(onYes,out OnYes, yesButton);
        SetActionAndButton(onNo, out OnNo, noButton);
        SetActionAndButton(onCancel, out OnCancel, cancelButton);
        SetActionAndButton(onOk, out OnOk, okButton);
        message.text = text;
        title.text = titleText;
        panel.SetActive(true);
    }

    public void SetActionAndButton(Action actionToAssign,out Action myAction, Button button)
    {
        myAction = actionToAssign;
        button.gameObject.SetActive(myAction==null?false:true);
    }

    public void OnClick_Yes()
    {
        OnYes?.Invoke();
        OnYes = null;
        panel.SetActive(false);
    }

    public void OnClick_No()
    {
        OnNo?.Invoke();
        OnNo = null;
        panel.SetActive(false);
    }

    public void OnClick_Cancel()
    {
        OnCancel?.Invoke();
        OnCancel = null;
        panel.SetActive(false);
    }

    public void OnClick_Ok()
    {
        OnOk?.Invoke();
        OnOk = null;
        panel.SetActive(false);
    }
}
