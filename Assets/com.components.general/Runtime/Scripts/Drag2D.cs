using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Drag2D : MonoBehaviour
{
	public UnityEvent onDrag;
	public UnityEvent onDragStart;
	public UnityEvent onDragEnd;

	private Vector3 screenPoint;
	private Vector3 offset;

	private void OnEnable()
	{
		Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
		BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
		boxCollider2D.size = S;
		/*boxCollider2D.offset = new Vector2((S.x / 2), 0);*/
	}

	void OnMouseDown()
	{
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		onDragStart?.Invoke();
	}

	private void OnMouseUp()
	{
		onDragEnd?.Invoke();
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
		onDrag?.Invoke();
	}
}