using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingPanel : SingletonBehaviourUI<LoadingPanel>
{
    public GameObject loadingPanel;
    public Text infoText;
    public GameObject loadingBar;
    public Image loadingBarImage;

    public float loadingPercentage { 
        get {
            return loadingBarImage.fillAmount*100.0f;
        } 
        set {
            loadingBarImage.fillAmount = value/100.0f;
        } }

    public UnityEvent onClose;

    public void Show(string text = "",bool showLoadingBar = false)
    {
        infoText.text = text;
        loadingBar.SetActive(showLoadingBar);
        loadingPanel.SetActive(true);
    }

    public void StartLoadingWithTime(float time)
    {
        StartCoroutine(TimerLoading(time));   
    }

    private IEnumerator TimerLoading(float time)
    {
        float val = time;
        while (time >= 0)
        {
            float adjust = time / val;
            loadingBarImage.fillAmount = 1.0f - adjust;
            time -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Hide()
    {
        loadingPanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
    }
}
