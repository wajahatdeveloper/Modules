using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Dialog : SingletonBehaviour<Dialog>
{
    public GameObject panel;
    
    public TextMeshProUGUI title;
    public TextMeshProUGUI message;

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
        
        Debug.Log($"Scene: {SceneManager.GetActiveScene().name}: <b>Dialog Panel</b> : Shown");
    }

    public void Hide()
    {
        OnYes = null;
        OnNo = null;
        OnCancel = null;
        OnOk = null;
        
        panel.SetActive(false);
    }

    private void SetActionAndButton(Action actionToAssign,out Action myAction, Button button)
    {
        myAction = actionToAssign;
        button.gameObject.SetActive(myAction==null?false:true);
    }

    public void OnClick_Yes()
    {
        OnYes?.Invoke();

        Hide();
        Debug.Log($"Scene: {SceneManager.GetActiveScene().name}: <b>Dialog Panel (OnYes)</b> : Hidden");
    }

    public void OnClick_No()
    {
        OnNo?.Invoke();

        Hide();
        Debug.Log($"Scene: {SceneManager.GetActiveScene().name}: <b>Dialog Panel (OnNo)</b> : Hidden");
    }

    public void OnClick_Cancel()
    {
        OnCancel?.Invoke();

        Hide();
        Debug.Log($"Scene: {SceneManager.GetActiveScene().name}: <b>Dialog Panel (OnCancel)</b> : Hidden");
    }

    public void OnClick_Ok()
    {
        OnOk?.Invoke();
        
        Hide();
        Debug.Log($"Scene: {SceneManager.GetActiveScene().name}: <b>Dialog Panel (OnOK)</b> : Hidden");
    }
}