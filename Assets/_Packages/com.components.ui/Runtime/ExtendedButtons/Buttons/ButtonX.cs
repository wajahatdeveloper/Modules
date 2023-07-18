using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonX : MonoBehaviour , IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public float clickCooldown = 0.1f;
    public bool cooldownRealtime = false;
    public bool debugLogEvents = false;
    public bool closeParentOnClick = false;
    
    public UnityEvent onEnter;
    public UnityEvent onDown;
    public UnityEvent onUp;
    public UnityEvent onExit;

    private Button _button;
    private bool _lockClick = false;

    private void Start()
    {
        _button = GetComponent<Button>();

        if (debugLogEvents)
        {
            _button.onClick.AddListener(()=>Debug.Log($"{gameObject.name} : On Pointer Click"));
        }
    }

    private IEnumerator RestoreLock()
    {
        if (cooldownRealtime)
        {
            yield return new WaitForSecondsRealtime(clickCooldown);
        }
        else
        {
            yield return new WaitForSeconds(clickCooldown);
        }

        _button.enabled = true;
        _lockClick = false;
    }

    private void EnableLock()
    {
        _lockClick = true;
        _button.enabled = false;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_button.enabled) return;
        if (!_button.interactable) return;
            
        if (_lockClick) { return; }
        EnableLock();
        StartCoroutine(RestoreLock());

        if (debugLogEvents)
        {
            Debug.Log($"{gameObject.name} : On Pointer Down");
        }
        
        onDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (debugLogEvents)
        {
            Debug.Log($"{gameObject.name} : On Pointer Up");
        }

        if (closeParentOnClick)
        {
            transform.parent.gameObject.SetActive(false);
        }
        
        onUp?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (debugLogEvents)
        {
            Debug.Log($"{gameObject.name} : On Pointer Enter");
        }
        
        onEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (debugLogEvents)
        {
            Debug.Log($"{gameObject.name} : On Pointer Exit");
        }
        
        onExit?.Invoke();
    }
}
