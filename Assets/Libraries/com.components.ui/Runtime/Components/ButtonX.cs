using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonX : MonoBehaviour , IPointerDownHandler, IPointerUpHandler,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [TitleGroup("Cooldown")]
    public float clickCooldown = 0.1f;
    public bool cooldownRealtime = false;

    [TitleGroup("Double Click")]
    public bool detectDoubleClick = false;
    [ShowIf(nameof(detectDoubleClick))]
    [Range (0.01f, 0.5f)]
    [Tooltip ("Duration between 2 clicks in seconds")]
    public float doubleClickDuration = 0.4f ;
    [Space]
    [ShowIf(nameof(detectDoubleClick))] public UnityEvent onDoubleClick ;

    [TitleGroup("Long Press")]
    public bool detectLongPress = false;
    [ShowIf(nameof(detectLongPress))]
    [Range(0.3f, 5f)]
    [Tooltip("Hold duration in seconds")]
    public float holdDuration = 0.5f;
    [Space]
    [ShowIf(nameof(detectLongPress))] public UnityEvent onLongPress;

    [TitleGroup("Pointer Events")]
    public bool usePointerEvents = false;
    [Space]
    [ShowIf(nameof(usePointerEvents))] public UnityEvent onEnter;
    [ShowIf(nameof(usePointerEvents))] public UnityEvent onDown;
    [ShowIf(nameof(usePointerEvents))] public UnityEvent onUp;
    [ShowIf(nameof(usePointerEvents))] public UnityEvent onExit;

    [TitleGroup("Additional Functionality")]
    public bool closeParentOnClick = false;
    public bool debugLogEvents = false;

    [HideInInspector] public Button button;

    // cooldown
    private bool _lockClick = false;

    // double click
    private byte clicks = 0;
    private DateTime firstClickTime;

    // long press
    private bool isPointerDown = false;
    private bool isLongPressed = false;
    private DateTime pressTime;
    private WaitForSeconds delay;

    private void Start()
    {
        button = GetComponent<Button>();
        delay = new WaitForSeconds(0.1f);
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

        // COOLDOWN
        if (_lockClick) { return; }
        EnableLock();
        StartCoroutine(RestoreLock());

        if (debugLogEvents) { Debug.Log($"{gameObject.name} : On Pointer Down"); }
        
        onDown?.Invoke();

        // LONG PRESS
        isPointerDown = true;
        pressTime = DateTime.Now;
        StartCoroutine(Routine_LongPressTimer());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (debugLogEvents) { Debug.Log($"{gameObject.name} : On Pointer Up"); }

        if (closeParentOnClick)
        {
            transform.parent.gameObject.SetActive(false);
        }
        
        onUp?.Invoke();

        // LONG PRESS
        isPointerDown = false;
        isLongPressed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (debugLogEvents) { Debug.Log($"{gameObject.name} : On Pointer Enter"); }
        
        onEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (debugLogEvents) { Debug.Log($"{gameObject.name} : On Pointer Exit"); }
        
        onExit?.Invoke();
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        if (debugLogEvents) { Debug.Log($"{gameObject.name} : On Pointer Click"); }

        // DOUBLE CLICK
        if (detectDoubleClick)
        {
            double elapsedSeconds = (DateTime.Now - firstClickTime).TotalSeconds;
            if (elapsedSeconds > doubleClickDuration)
                clicks = 0;

            clicks++ ;

            if (clicks == 1)
                firstClickTime = DateTime.Now;
            else if (clicks > 1) {
                if (elapsedSeconds <= doubleClickDuration) {
                    if (button.interactable)
                        onDoubleClick?.Invoke();
                }
                clicks = 0;
            }
        }
    }

    private IEnumerator Routine_LongPressTimer()
    {
        while (isPointerDown && !isLongPressed) {
            double elapsedSeconds = (DateTime.Now - pressTime).TotalSeconds;

            if (elapsedSeconds >= holdDuration) {
                isLongPressed = true;
                if (button.interactable)
                    onLongPress?.Invoke();

                yield break;
            }

            yield return delay;
        }
    }
}