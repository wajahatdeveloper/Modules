using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabSwitchFocus : MonoBehaviour
{
    private EventSystem _system;
 
    void Start ()
    {
        _system = EventSystem.current;
    }
        
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            var currentGameObject = _system.currentSelectedGameObject;
            if (currentGameObject == null) { return; }
            
            Selectable next = currentGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next == null) return;
            
            InputField inputfield = next.GetComponent<InputField>();
            if (inputfield != null)
            {
                inputfield.OnPointerClick(new PointerEventData(_system)); //if it's an input field, also set the text caret
                _system.SetSelectedGameObject(next.gameObject, new BaseEventData(_system));
            }
            else
            {
                TMP_InputField tmpInputField = next.GetComponent<TMP_InputField>();
                if (tmpInputField != null)
                {
                    tmpInputField.OnPointerClick(new PointerEventData(_system)); //if it's an input field, also set the text caret
                    _system.SetSelectedGameObject(next.gameObject, new BaseEventData(_system));
                }
            }
        }
    }
}
