using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AutoUIRefs))]
public class UITheme : MonoBehaviour
{
	public UIThemeScriptable themeData;

	private AutoUIRefs autoUIRefs;

	[Button]
	[ContextMenu(nameof(ApplyTheme))]
	public void ApplyTheme()
	{
		autoUIRefs = GetComponent<AutoUIRefs>();
		IterateThroughChildren(gameObject);

		// Find and Apply all Theme Overrides
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

			// Apply Theme to components
			ApplyTheme_Buttons(autoUIRefs.buttons);

			// If the child has its own children, recurse into them
			if (child.childCount > 0)
			{
				IterateChildrenRecursive(child);
			}
		}
	}

	private void ApplyTheme_Buttons(List<Button> buttons)
	{
		foreach (Button button in buttons)
		{

		}
	}
}