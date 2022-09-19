using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Drop2D : MonoBehaviour
{
	public UnityEvent<GameObject> onDrop;

	[Header("Debug")]
	public GameObject droppedObject;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		droppedObject = collision.gameObject;
	}

	private void Update()
	{
		if (droppedObject == null) { return; }
		if (droppedObject.GetComponent<Drag2D>() == null) { return; }
		if (Input.GetMouseButtonUp(0))
		{
			onDrop?.Invoke(droppedObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		droppedObject = null;
	}
}