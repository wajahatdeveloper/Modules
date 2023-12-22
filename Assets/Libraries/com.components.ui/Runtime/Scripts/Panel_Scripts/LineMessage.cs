using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LineMessage : SingletonBehaviour<LineMessage>
{
    public GameObject messagePrefab;

    public void Show(string message, string titleString = "", float time = 4.0f)
    {
        var messageLine = Instantiate(messagePrefab,transform);
        var titleText = messageLine.transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // Title
        var messageText = messageLine.transform.GetChild(1).GetComponent<TextMeshProUGUI>(); // Message
        
        messageText.text = message;
        titleText.text = titleString;
        
        Destroy(messageLine,time);
    }
}