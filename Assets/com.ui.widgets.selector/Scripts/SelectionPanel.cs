using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPanel : MonoBehaviour
{
    [Header("Input")]
    public List<GameObject> items;
    
    [Header("Optional Inputs")]
    public GameObject panel;
    public string prefString;
    public bool doHideOnSelection;
    public bool warpItems;

    [Header("Output")]
    public int currentIndex;
    public UnityEngine.Events.UnityEvent onItemSelected;

    public int PrefValue { 
        get => PlayerPrefs.GetInt(prefString,0);
        set => PlayerPrefs.SetInt(prefString, value);
    }

    public void Show()
    {
        items.ForEach(item => item.SetActive(false));
        items[0].SetActive(true);
        currentIndex = 0;
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    public void OnClick_Select_Item()
    {
        PrefValue = currentIndex;
        onItemSelected?.Invoke();
        if (doHideOnSelection)
        {
            Hide();
        }
    }

    public void OnClick_Prev_Item()
    {
        items[currentIndex].SetActive(false);
        if (warpItems)
        {
            currentIndex = (currentIndex - 1) < 0 ? items.Count-1:currentIndex-1;
        }
        else
        {
            currentIndex = (currentIndex - 1) <= 0 ? 0:currentIndex-1;
        }
        items[currentIndex].SetActive(true);
    }

    public void OnClick_Next_Item()
    {
        items[currentIndex].SetActive(false);
        if (warpItems)
        {
            currentIndex = (currentIndex + 1) >= items.Count ? 0 : currentIndex + 1;
        }
        else
        {
            currentIndex = (currentIndex + 1) >= (items.Count - 1) ? (items.Count - 1) : currentIndex + 1;
        }
        items[currentIndex].SetActive(true);
    }

}
