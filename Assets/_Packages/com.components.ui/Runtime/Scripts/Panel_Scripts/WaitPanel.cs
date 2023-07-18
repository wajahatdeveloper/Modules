using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WaitPanel : SingletonBehaviourUI<WaitPanel>
{
    public GameObject waitPanel;
    public Text waitingText;
    public UnityEvent onClose;

    private int _count = 0;
    
    public void Show(string text="")
    {
        if (waitPanel == null)
        {
            Debug.LogError("Wait panel game object not assigned");
        }
        else
        {
            Debug.Log("Wait Panel Shown");
            waitingText.text = text;
            waitPanel.SetActive(true);
        }
    }
    
    public void ShowCounted(string text = "")
    {
        if (waitPanel == null)
        {
            Debug.LogError("Wait panel game object not assigned");
        }
        else
        {
            _count++;
            Debug.Log("Wait Panel Shown : count = " + _count);
            waitingText.text = text;
            waitPanel.SetActive(true);
        }
    }

    public void HideCounted()
    {
        _count--;
        if (_count <= 0)
        {
            Hide();
        }
    }
    
    public void Hide()
    {
        Debug.Log("Wait Panel Hidden");
        waitPanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
    }
}