using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(ButtonX))]
public class ButtonXEditor : Editor
{
    public override void OnInspectorGUI()
    {
       // ButtonX buttonX = (ButtonX)target;

        // Display the default Inspector for ButtonX script
        DrawDefaultInspector();

        // Display runtime listeners of the Button component
        /*if (buttonX.button != null)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Runtime Button Listeners", EditorStyles.boldLabel);

            SerializedObject buttonObj = new SerializedObject(buttonX.button);
            SerializedProperty onClickProp = buttonObj.FindProperty("m_OnClick");


            // Iterate through the UnityEvent listeners
            for (int i = 0; i < onClickProp.arraySize; i++)
            {
                SerializedProperty listener = onClickProp.GetArrayElementAtIndex(i);
                SerializedProperty method = listener.FindPropertyRelative("m_MethodName");
                SerializedProperty targetObj = listener.FindPropertyRelative("m_Target");

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Listener " + i);
                EditorGUILayout.PropertyField(targetObj, GUIContent.none);
                EditorGUILayout.PropertyField(method, GUIContent.none);
                EditorGUILayout.EndHorizontal();
            }

            buttonObj.ApplyModifiedProperties();
        }*/
    }
}