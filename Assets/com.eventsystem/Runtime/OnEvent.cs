using UnityEngine;
using UnityEngine.Events;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using System.Collections.Generic;
using System;

public class OnEvent : MonoBehaviour
{
	#if ODIN_INSPECTOR
	private static IEnumerable<string> EventsIdentifiers = Enum.GetNames( typeof( Events.EventsIdentifier ) );

	[ValueDropdown( "EventsIdentifiers" , IsUniqueList = true , DisableListAddButtonBehaviour = true , NumberOfItemsBeforeEnablingSearch = 1 )]
	#endif
	public string eventIdentifier;
	public UnityEvent OnEventReceived;
	public UnityEvent<string> OnEventReceived_String;

	private void OnEnable()
	{
		if (eventIdentifier == "")
		{
			Debug.LogError( $"{name} is missing Event Receive Identifier" );
		}
		gameObject.ConnectEvent( eventIdentifier, EventCall );
	}

	private void EventCall( GameObject sender, object eventData )
	{
		if (eventData == null) { if(OnEventReceived!=null){OnEventReceived.Invoke();} }
		string str = eventData as string;
		OnEventReceived_String?.Invoke( str );
	}

	private void OnDisable()
	{
		gameObject.DisconnectEvent( eventIdentifier );
	}
}