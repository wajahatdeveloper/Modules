using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine.UI;

[CustomEditor(typeof(AutoUIRefs))]
public class AutoUIRefsEditor : Editor
{
	private AutoUIRefs uiManager;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		uiManager = (AutoUIRefs)target;

		if (GUILayout.Button("Find UI Elements"))
		{
			uiManager.FindUIElements(uiManager.gameObject);
		}

		if (GUILayout.Button("Generate Script"))
		{
			GenerateUIManagerScript();
		}
	}

	private void GenerateUIManagerScript()
	{
		string path = AssetDatabase.GetAssetPath(MonoScript.FromMonoBehaviour((MonoBehaviour)target));
		string folderPath = Path.GetDirectoryName(path);
		string scriptPath = Path.Combine(folderPath, $"Generated/{uiManager.scriptName}.cs");

		// Generate script content
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("using System;");
		sb.AppendLine("using TMPro;");
		sb.AppendLine("using UnityEngine;");
		sb.AppendLine("using UnityEngine.UI;");
		sb.AppendLine();
		sb.AppendLine($"public class {uiManager.scriptName} : MonoBehaviour");
		sb.AppendLine("{");

		// Add fields for each UI component type
		AppendFields(sb, "Button", uiManager.buttons.Select(x=>x as Component).ToList());
		AppendFields(sb, "ButtonX", uiManager.buttonXes.Select(x=>x as Component).ToList());
		AppendFields(sb, "Text", uiManager.texts.Select(x=>x as Component).ToList());
		AppendFieldsTMP(sb, "TextMeshProUGUI", uiManager.textMeshes);
		AppendFields(sb, "Image", uiManager.images.Select(x=>x as Component).ToList());
		AppendFields(sb, "Toggle", uiManager.toggles.Select(x=>x as Component).ToList());
		AppendFields(sb, "InputField", uiManager.inputFields.Select(x=>x as Component).ToList());
		AppendFields(sb, "Slider", uiManager.sliders.Select(x=>x as Component).ToList());
		AppendFields(sb, "Dropdown", uiManager.dropdowns.Select(x=>x as Component).ToList());
		AppendFields(sb, "ScrollRect", uiManager.scrollRects.Select(x=>x as Component).ToList());

		sb.AppendLine("}");

		// Write script to file
		File.WriteAllText(scriptPath, sb.ToString());
		AssetDatabase.ImportAsset(scriptPath);

		Debug.Log($"Generated script at {scriptPath}");
	}

	// Helper method to append fields for a specific UI component type
	private void AppendFields(StringBuilder sb, string componentType, List<Component> components)
	{
		foreach (Component component in components)
		{
			sb.AppendLine($"    [AutoRef(AutoRefTargetType.Children)]");
			sb.AppendLine($"    public {componentType} {component.gameObject.name};");
		}
	}

	private void AppendFieldsTMP(StringBuilder sb, string componentType, List<TextMeshProUGUI> components)
	{
		foreach (TextMeshProUGUI component in components)
		{
			sb.AppendLine($"    [AutoRef(AutoRefTargetType.Children)]");
			sb.AppendLine($"    public {componentType} {component.gameObject.name};");
		}
	}
}