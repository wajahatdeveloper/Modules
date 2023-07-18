using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesNoPanel : SingletonBehaviourUI<YesNoPanel>
{
    public GameObject panel;
    public Text message;

    public Action OnYes;
    public Action OnNo;
    
    public void Show(string text)
    {
        message.text = text;
        panel.SetActive(true);
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
}
