using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UltEvents;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonX : MonoBehaviour,
		IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,
		IPointerEnterHandler, IPointerExitHandler
{
	[ShowInInspector, PropertyOrder(0)]
	public bool Interactable
	{
		get => isInteractable;
		set
		{
			isInteractable = value;
			_SetInteractable(isInteractable);
		}
	}

	[FoldoutGroup("Tint Color"), PropertyOrder(1)] public Color normalColor = Color.white;
	[FoldoutGroup("Tint Color"), PropertyOrder(1)] public Color highlightColor = Color.cyan;
	[FoldoutGroup("Tint Color"), PropertyOrder(1)] public Color pressedColor = Color.gray;
	[FoldoutGroup("Tint Color"), PropertyOrder(1)] public Color disabledColor = Color.black;

	[InlineEditor, PropertyOrder(2)]
	public Image image;

	[InlineEditor, PropertyOrder(3)]
	public TextMeshProUGUI textMesh;

	[PropertyOrder(4)]
	public UltEvent onClick;

	private bool isInteractable = true;

	private void _SetInteractable(bool interactable)
	{
		image.color = interactable ? normalColor : disabledColor;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!isActiveAndEnabled || !isInteractable) { return; }
		onClick.InvokeSafe();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!isActiveAndEnabled || !isInteractable) { return; }
		image.color = pressedColor;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!isActiveAndEnabled || !isInteractable) { return; }
		image.color = normalColor;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!isActiveAndEnabled || !isInteractable) { return; }
		image.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isActiveAndEnabled || !isInteractable) { return; }
		image.color = normalColor;
	}
}