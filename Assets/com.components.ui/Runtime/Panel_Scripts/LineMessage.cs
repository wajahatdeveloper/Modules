using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LineMessage : SingletonBehaviourUI<LineMessage>
{
    public GameObject messagePrefab;

    public void Show(string message, string titleString = "", float time = 4.0f)
    {
        var messageLine = Instantiate(messagePrefab,transform);
        var titleText = messageLine.transform.GetChild(0).GetComponent<Text>(); // Title
        var messageText = messageLine.transform.GetChild(1).GetComponent<Text>(); // Message
        
        messageText.text = message;
        titleText.text = titleString;
        
        Destroy(messageLine,time);
    }
}
