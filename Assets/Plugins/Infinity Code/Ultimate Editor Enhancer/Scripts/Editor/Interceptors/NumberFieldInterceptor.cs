/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Globalization;
using System.Reflection;
using HarmonyLib;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.Interceptors
{
    [InitializeOnLoad]
    public static class NumberFieldInterceptor
    {
        private static string recycledText;
        private static Harmony harmony;
        private static MethodInfo prefixMethod;

        static NumberFieldInterceptor()
        {
            Type[] parameters2 = {
                typeof(object),
                typeof(Rect),
                typeof(Rect),
                typeof(int),
#if !UNITY_2021_2_OR_NEWER
                typeof(bool),
                typeof(double).MakeByRefType(),
                typeof(long).MakeByRefType(),
#else
                typeof(EditorGUIRef.NumberFieldValue).MakeByRefType(),
#endif
                typeof(string),
                typeof(GUIStyle),
                typeof(bool),
                typeof(double)
            };

            prefixMethod = AccessTools.Method(typeof(NumberFieldInterceptor), "DoNumberFieldPrefix", parameters2);

            if (Prefs.changeNumberFieldValueByArrow) Patch();
        }

        private static void DoNumberFieldPrefix(
            object editor,
            Rect position,
            Rect dragHotZone,
            int id,
#if !UNITY_2021_2_OR_NEWER
            bool isDouble,
            ref double doubleVal,
            ref long longVal,
#else
            ref EditorGUIRef.NumberFieldValue value,
#endif
            string formatString,
            GUIStyle style,
            bool draggable,
            double dragSensitivity)
        {
            Event e = Event.current;
            int v = 0;

            if (Prefs.changeNumberFieldValueByArrow && e.type == EventType.KeyDown && GUIUtility.keyboardControl == id)
            {
                if (e.keyCode == KeyCode.UpArrow)
                {
                    if (e.control || e.command) v = 100;
                    else if (e.shift) v = 10;
                    else v = 1;

                    e.Use();
                }
                else if (e.keyCode == KeyCode.DownArrow)
                {
                    if (e.control || e.command) v = -100;
                    else if (e.shift) v = -10;
                    else v = -1;
                    e.Use();
                }

                if (v != 0)
                {
#if !UNITY_2021_2_OR_NEWER
                    if (isDouble)
                    {
                        if (!double.IsInfinity(doubleVal) && !double.IsNaN(doubleVal))
                        {
                            doubleVal += v;
                            recycledText = doubleVal.ToString(Culture.numberFormat);
                            GUI.changed = true;
                        }
                    }
                    else
                    {
                        longVal += v;
                        recycledText = longVal.ToString();
                        GUI.changed = true;
                    }
#else 
                    if (value.isDouble)
                    {
                        if (!double.IsInfinity(value.doubleVal) && !double.IsNaN(value.doubleVal))
                        {
                            value.doubleVal += v;
                            value.success = true;
                            recycledText = value.doubleVal.ToString(Culture.numberFormat);
                            GUI.changed = true;
                        }
                    }
                    else
                    {
                        value.longVal += v;
                        value.success = true;
                        recycledText = value.longVal.ToString();
                        GUI.changed = true;
                    }
#endif

                    TextEditorRef.SetText(editor, recycledText);
                    TextEditorRef.SetCursorIndex(editor, 0);
                    TextEditorRef.SetSelectionIndex(editor, recycledText.Length);
                }
            }
        }

        private static void Patch()
        {
            if (harmony != null) return;

            try
            {
                harmony = new Harmony("InfinityCode.UltimateEditorEnhancer.NumberFieldInterceptor");
                harmony.Patch(EditorGUIRef.doNumberFieldMethod, new HarmonyMethod(prefixMethod));
            }
            catch
            {
            }
        }

        public static void Refresh()
        {
            if (Prefs.changeNumberFieldValueByArrow) Patch();
            else
            {
                harmony.Unpatch(EditorGUIRef.doNumberFieldMethod, prefixMethod);
                harmony = null;
            }
        }
    }
}