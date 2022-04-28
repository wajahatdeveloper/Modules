/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InfinityCode.UltimateEditorEnhancer.Interceptors;
using InfinityCode.UltimateEditorEnhancer.PropertyDrawers;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using InfinityCode.UltimateEditorEnhancer.Windows;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer.InspectorTools
{
    [InitializeOnLoad]
    public static class NestedEditor
    {
        public static bool disallowNestedEditors;

        private static Dictionary<int, bool> disallowCache;

        private static GUIContent content;

        static NestedEditor()
        {
            disallowCache = new Dictionary<int, bool>();
            ObjectFieldDrawer.OnGUIAfter += OnGUIAfter;
        }

        private static void OnGUIAfter(Rect area, SerializedProperty property, GUIContent label)
        {
            if (!Prefs.nestedEditors) return;
            if (disallowNestedEditors) return;

            Object obj = property.objectReferenceValue;
            if (obj == null) return;

            if (!ReorderableListInterceptor.insideList)
            {
                bool disallow;
                int id = property.serializedObject.targetObject.GetInstanceID() ^ property.propertyPath.GetHashCode();

                if (!disallowCache.TryGetValue(id, out disallow))
                {
                    Type type = property.serializedObject.targetObject.GetType();
                    FieldInfo field = type.GetField(property.name, Reflection.InstanceLookup);
                    if (field != null)
                    {
                        disallowCache[id] = disallow = field.GetCustomAttribute<DisallowNestedEditor>() != null;
                        if (disallow) return;
                    }
                    else disallowCache[id] = false;
                }
                else if (disallow) return;
            }

            area.xMin += EditorGUI.indentLevel * 15 - 16;
            area.width = 16;

            Color color = GUI.color;

            Vector2 mousePosition = Event.current.mousePosition;
            if (area.Contains(mousePosition))
            {
                GUI.color = Color.gray;
            }

            if (content == null)
            {
                content = new GUIContent(EditorIconContents.editIcon);
            }

            StaticStringBuilder.Clear();
            StaticStringBuilder.Append("Open ");
            StaticStringBuilder.Append(obj.name);

            if (obj is Component)
            {
                StaticStringBuilder.Append(" (");
                StaticStringBuilder.Append(ObjectNames.NicifyVariableName(obj.GetType().Name));
                StaticStringBuilder.Append(")");
            }

            StaticStringBuilder.Append(" in window");

            content.tooltip = StaticStringBuilder.GetString(true);

            if (GUI.Button(area, content, GUIStyle.none))
            {
                if (obj is Component) ComponentWindow.Show(obj as Component, false).closeOnLossFocus = false;
                else if (obj is GameObject) PropertyEditorRef.OpenPropertyEditor(obj);
                else ObjectWindow.Show(new []{obj}, false).closeOnLossFocus = false;
            }

            GUI.color = color;
        }
    }
}