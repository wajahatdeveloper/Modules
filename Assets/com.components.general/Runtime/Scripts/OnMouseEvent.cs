using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMouseEvent : MonoBehaviour
{
	public UnityEvent onMouseUp;
	public UnityEvent onMouseDown;
	public UnityEvent onMouseOver;
	public UnityEvent onMouseEnter;
	public UnityEvent onMouseExit;
	public UnityEvent onMouseDrag;
	public UnityEvent onMouseUpAsButton;

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