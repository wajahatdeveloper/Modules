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
		foreach (Button button in autoUIRefs.buttons)
		{
			var themedItem = button.GetComponent<UIThemedItem>();
			if (themedItem == null)
			{
				themedItem = button.gameObject.AddComponent<UIThemedItem>();
				themedItem.Apply();
			}
		}
	}
}