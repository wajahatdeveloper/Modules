using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingPanel : SingletonBehaviour<LoadingPanel>
{
    public GameObject loadingPanel;
    public Text infoText;
    public UnityEvent onClose;

    public void Show(string text = "")
    {
        infoText.text = text;
        loadingPanel.SetActive(true);
    }

    public void Hide()
    {
        loadingPanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
    }
}
