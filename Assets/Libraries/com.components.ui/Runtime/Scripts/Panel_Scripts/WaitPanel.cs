using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitPanel : SingletonBehaviour<WaitPanel>
{
    public GameObject waitPanel;
    public TextMeshProUGUI waitingText;
    public UnityEvent onClose;

    private int _count = 0;
    
    public void Show(string text="Please Wait..")
    {
        if (waitPanel == null)
        {
            Debug.LogError($"Scene: {SceneManager.GetActiveScene().name}: <b>Wait Panel</b> game object not assigned");
        }
        else
        {
            Debug.Log($"Scene: {SceneManager.GetActiveScene().name}: <b>Wait Panel</b> Shown");
            waitingText.text = text;
            waitPanel.SetActive(true);
        }
    }
    
    public void ShowCounted(string text = "")
    {
        if (waitPanel == null)
        {
            Debug.LogError($"Scene: {SceneManager.GetActiveScene().name}: <b>Wait Panel</b> game object not assigned");
        }
        else
        {
            _count++;
            Debug.Log($"Scene: {SceneManager.GetActiveScene().name}: <b>Wait Panel</b> Shown : count = " + _count);
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
        Debug.Log($"Scene: {SceneManager.GetActiveScene().name}: <b>Wait Panel</b> Hidden");
        waitPanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
    }
}