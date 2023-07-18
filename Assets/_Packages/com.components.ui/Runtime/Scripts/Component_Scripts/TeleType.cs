using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeleType : MonoBehaviour
{
    public bool onlyOnce;
    private TextMeshProUGUI _textMeshProUGUI;

    private IEnumerator Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _textMeshProUGUI.maxVisibleCharacters = 0;
        
        yield return null;
        
        int totalVisibleCharacters = _textMeshProUGUI.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshProUGUI.maxVisibleCharacters = visibleCount;
            if (visibleCount >= totalVisibleCharacters)
            {
                if (onlyOnce)
                {
                    yield break;
                }
                else
                {
                    yield return new WaitForSeconds(1.0f);
                }
            }

            counter += 1;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
