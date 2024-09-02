using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using TMPro;

public class AutoUIRefs : MonoBehaviour
{
	public string scriptName;

	public List<Button> buttons = new List<Button>();
	public List<ButtonX> buttonXes = new List<ButtonX>();
	public List<Text> texts = new List<Text>();
	public List<TextMeshProUGUI> textMeshes = new List<TextMeshProUGUI>();
	public List<Image> images = new List<Image>();
	public List<Toggle> toggles = new List<Toggle>();
	public List<InputField> inputFields = new List<InputField>();
	public List<Slider> sliders = new List<Slider>();
	public List<Dropdown> dropdowns = new List<Dropdown>();
	public List<ScrollRect> scrollRects = new List<ScrollRect>();

	private void OnValidate()
	{
		if (scriptName == String.Empty)
		{
			scriptName = gameObject.name + "UIComponents";
		}
	}

	public void FindUIElements(GameObject root)
	{
		buttons.Clear();
		buttonXes.Clear();
		texts.Clear();
		textMeshes.Clear();
		images.Clear();
		toggles.Clear();
		inputFields.Clear();
		sliders.Clear();
		dropdowns.Clear();
		scrollRects.Clear();
		IterateThroughChildren(root);
	}

	// Public method to start the recursive iteration
	public void IterateThroughChildren(GameObject root)
	{
		if (root != null)
		{
			IterateChildrenRecursive(root.transform);
		}
		else
		{
			Debug.LogWarning("Root GameObject is null.");
		}
	}

	// Recursive function to iterate through all children
	private void IterateChildrenRecursive(Transform parent)
	{
		foreach (Transform child in parent)
		{
			// Perform operations with the child GameObject
			Debug.Log("Child GameObject: " + child.name);

			// Add UI components to respective lists
			AddComponentToList<Button>(child, buttons);
			AddComponentToList<ButtonX>(child, buttonXes);
			AddComponentToList<Text>(child, texts);
			AddComponentToList<TextMeshProUGUI>(child, textMeshes);
			AddComponentToList<Image>(child, images);
			AddComponentToList<Toggle>(child, toggles);
			AddComponentToList<InputField>(child, inputFields);
			AddComponentToList<Slider>(child, sliders);
			AddComponentToList<Dropdown>(child, dropdowns);
			AddComponentToList<ScrollRect>(child, scrollRects);

			// If the child has its own children, recurse into them
			if (child.childCount > 0)
			{
				IterateChildrenRecursive(child);
			}
		}
	}

	// Generic method to add components to a list
	private void AddComponentToList<T>(Transform child, List<T> list) where T : Component
	{
		T component = child.GetComponent<T>();
		if (component != null)
		{
			// Don't fetch button texts
			if (component is TextMeshProUGUI || component is Text)
			{
				if (component.GetComponentInParent<Button>() != null ||
				    component.GetComponentInParent<ButtonX>() != null)
				{
					return;
				}
			}

			// Don't fetch button images
			if (component is Image)
			{
				if (component.GetComponentInParent<Button>() != null ||
				    component.GetComponentInParent<ButtonX>() != null)
				{
					return;
				}
			}

			list.Add(component);
			Debug.Log($"Added {typeof(T).Name} to list: {component.name}");
		}
	}
}