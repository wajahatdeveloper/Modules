using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

public class RaiseEvent : MonoBehaviour
{
    /*#if ODIN_INSPECTOR
	private static IEnumerable<string> EventsIdentifiers = Enum.GetNames( typeof( Events.EventsIdentifier ) );

	[ValueDropdown( "EventsIdentifiers", IsUniqueList = true, DisableListAddButtonBehaviour = true, NumberOfItemsBeforeEnablingSearch = 1 )]
    #endif*/
    
	public string eventIdentifier;

    public bool autoHookButton = true;

    private void Start()
    {
        if (autoHookButton)
        {
            GetComponent<Button>()?.onClick.AddListener( RaiseDefaultEvent );
        }
    }

    public void RaiseDefaultEvent()
    {
        gameObject.RaiseEvent(eventIdentifier);
    }
    
    public void RaiseCustomEvent(string evtId)
    {
        gameObject.RaiseEvent(evtId);
    }
}
