using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanel : SingletonBehaviour<LoadingPanel>
{
    public GameObject loadingPanel;
    public TextMeshProUGUI infoText;
    public GameObject loadingBar;
    public Image loadingBarImage;

    private static int _counter = 0;

    public float loadingPercentage
    {
        get { return loadingBarImage.fillAmount * 100.0f; }
        set { loadingBarImage.fillAmount = value / 100.0f; }
    }

    public UnityEvent onClose;

    public void Show(string text = "Loading..", bool showLoadingBar = false)
    {
        infoText.text = text;
        loadingBar.SetActive(showLoadingBar);
        loadingPanel.SetActive(true);

        Debug.Log($"{SceneManager.GetActiveScene().name}: <b>Loading Panel</b> : Shown id:{_counter}");

        _counter++;
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
        Debug.Log($"{SceneManager.GetActiveScene().name}: <b>Loading Panel</b> : Hidden id:{_counter}");

        loadingPanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();

        _counter--;
    }

    public void HideIfShown()
    {
        if (loadingPanel.activeSelf)
        {
            Hide();
        }
    }
}