using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DynamicRangeSections))]
public class DynamicRangeSectionsEditor : Editor
{
    private bool isShown = true;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        isShown = EditorGUILayout.Foldout(isShown, "Dynamic Range Sections");
        if (!isShown) { return; }

        var self = (DynamicRangeSections)target;

        float totalRange = self.rangeSections.Sum(x => x.rangeValue);
        float rangeOffset = 0f;

        Rect totalRect = EditorGUILayout.BeginHorizontal();
        float oneUnitWidth = totalRect.width / totalRange;

        for (var i = 0; i < self.rangeSections.Count; i++)
        {
            var rangeSection = self.rangeSections[i];

            // Draw the color bar
            Rect colorBarRect = GUILayoutUtility.GetRect(0, 20);
            colorBarRect.width = rangeSection.rangeValue * oneUnitWidth;
            EditorGUI.DrawRect(colorBarRect, rangeSection.rangeColor);

            // Draw the label
            Rect labelRect = GUILayoutUtility.GetRect(40, 20); // Adjust the width based on your needs
            GUI.contentColor = ModifyHue(rangeSection.rangeColor);
            EditorGUI.LabelField(labelRect, rangeSection.rangeName);

            rangeOffset += rangeSection.rangeValue;
        }

        GUILayout.Space(10f);

        GUILayout.EndHorizontal();
    }

    private Color ModifyHue(Color originalColor)
    {
        // Convert RGB to HSL
        Color.RGBToHSV(originalColor, out float hue, out float saturation, out float lightness);

        // Modify the hue using the given formula
        hue = (hue + 180f) % 360f;

        // Convert back to RGB
        return Color.HSVToRGB(hue, saturation, lightness);
    }
}