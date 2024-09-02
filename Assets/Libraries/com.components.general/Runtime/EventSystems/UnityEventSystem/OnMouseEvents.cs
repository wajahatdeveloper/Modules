using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;

namespace UltEvents
{
	[AddComponentMenu(UltEventUtils.ComponentMenuPrefix + "On Mouse Events")]
	public class OnMouseEvents : MonoBehaviour
    {
    	public UltEvent onMouseUp;
    	public UltEvent onMouseDown;
    	public UltEvent onMouseOver;
    	public UltEvent onMouseEnter;
    	public UltEvent onMouseExit;
    	public UltEvent onMouseDrag;
    	public UltEvent onMouseUpAsButton;

    	private void OnMouseDown()
    	{
    		onMouseDown?.Invoke();
    	}

    	private void OnMouseUp()
    	{
    		onMouseUp?.Invoke();
    	}

    	private void OnMouseEnter()
    	{
    		onMouseEnter?.Invoke();
    	}

    	private void OnMouseExit()
    	{
    		onMouseExit?.Invoke();
    	}

    	private void OnMouseDrag()
    	{
    		onMouseDrag?.Invoke();
    	}

    	private void OnMouseOver()
    	{
    		onMouseOver?.Invoke();
    	}

    	private void OnMouseUpAsButton()
    	{
    		onMouseUpAsButton?.Invoke();
    	}
    }
}