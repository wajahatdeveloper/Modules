using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonX : MonoBehaviour , IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Cooldown")]
    public float clickCooldown = 0.1f;
    public bool cooldownRealtime = false;

    [Header("Additional Functionality")]
    public bool closeParentOnClick = false;
    public bool debugLogEvents = false;

    [Space]

    public UnityEvent onEnter;
    public UnityEvent onDown;
    public UnityEvent onUp;
    public UnityEvent onExit;

    [HideInInspector] public Button button;

    private bool _lockClick = false;

    private void Start()
    {
        button = GetComponent<Button>();

        if (debugLogEvents)
        {
            button.onClick.AddListener(()=>Debug.Log($"{gameObject.name} : On Pointer Click"));
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

        button.enabled = true;
        _lockClick = false;
    }

    private void EnableLock()
    {
        _lockClick = true;
        button.enabled = false;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!button.enabled) return;
        if (!button.interactable) return;
            
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