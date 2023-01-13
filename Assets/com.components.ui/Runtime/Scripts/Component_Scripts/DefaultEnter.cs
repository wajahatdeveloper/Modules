using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultEnter : MonoBehaviour
{
    public Button defaultButton;
    public float startDelay = 1.0f;
    public float resetDelay = 1.0f;

    private bool _shouldUpdate = false;
    private bool _clicked = false;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(startDelay);
        _shouldUpdate = true;
        _clicked = false;
    }

    private void Update()
    {
        if (!_shouldUpdate) { return; }
        if (_clicked) { return; }
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (defaultButton.interactable && defaultButton.enabled)
            {
                _clicked = true;
                defaultButton.onClick?.Invoke();
                StartCoroutine(ResetClick());
            }
        }
    }

    private IEnumerator ResetClick()
    {
        yield return new WaitForSeconds(resetDelay);
        _clicked = false;
    }
}
