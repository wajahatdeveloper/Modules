using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UGUITheme
{
	[CreateAssetMenu(menuName = "UI/ThemeTags")]
	public class UIThemeTagsScriptable : ScriptableObject
	{
		[SerializeField] public List<string> themeTags = new();
	}
}