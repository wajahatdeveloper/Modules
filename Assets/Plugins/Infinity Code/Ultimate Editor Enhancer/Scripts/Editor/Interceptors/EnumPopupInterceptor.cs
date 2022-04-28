/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using HarmonyLib;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using InfinityCode.UltimateEditorEnhancer.Windows;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.Interceptors
{
    [InitializeOnLoad]
    public static class EnumPopupInterceptor
    {
        private static Harmony harmony;
        private static MethodInfo prefixMethod;

        static EnumPopupInterceptor()
        {
            if (Prefs.searchInEnumFields) Patch();
        }

        private static void DoPopupPrefix(
            Rect position,
            int controlID,
            int selected,
            GUIContent[] popupValues,
            Func<int, bool> checkEnabled,
            GUIStyle style)
        {
            Event e = Event.current;
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0 && position.Contains(e.mousePosition))
                    {
                        if (Application.platform == RuntimePlatform.OSXEditor)
                        {
                            position.y = (float)(-19.0 + position.y - selected * 16);
                        }

                        if (popupValues.Length >= Prefs.searchInEnumFieldsMinValues)
                        {
                            object instance = Activator.CreateInstance(PopupCallbackInfoRef.type, controlID);
                            PopupCallbackInfoRef.SetInstance(instance);
                            FlatSelectorWindow.Show(position, popupValues, EditorGUI.showMixedValue ? -1 : selected).OnSelect += i => { PopupCallbackInfoRef.GetSetEnumValueDelegate(instance).Invoke(null, null, i); };
                            GUIUtility.keyboardControl = controlID;
                            e.Use();
                        }
                    }
                    break;
                case EventType.KeyDown:
                    if (MainActionKeyForControl(e, controlID))
                    {
                        if (Application.platform == RuntimePlatform.OSXEditor)
                        {
                            position.y = (float)(-19.0 + position.y - selected * 16);
                        }

                        if (popupValues.Length >= Prefs.searchInEnumFieldsMinValues)
                        {
                            object instance = Activator.CreateInstance(PopupCallbackInfoRef.type, controlID);
                            PopupCallbackInfoRef.SetInstance(instance);
                            FlatSelectorWindow.Show(position, popupValues, EditorGUI.showMixedValue ? -1 : selected).OnSelect += i => { PopupCallbackInfoRef.GetSetEnumValueDelegate(instance).Invoke(null, null, i); };
                            e.Use();
                        }
                    }
                    break;
            }
        }

        internal static bool MainActionKeyForControl(Event e, int controlId)
        {
            if (GUIUtility.keyboardControl != controlId) return false;
            bool flag = e.alt || e.shift || e.command || e.control;
            return e.type == EventType.KeyDown && (e.keyCode == KeyCode.Space || e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter) && !flag;
        }

        private static void Patch()
        {
            if (harmony != null) return;

            try
            {
                harmony = new Harmony("InfinityCode.UltimateEditorEnhancer.EnumPopupInterceptor");

                Type[] parameters = {
                    typeof(Rect),
                    typeof(int),
                    typeof(int),
                    typeof(GUIContent[]),
                    typeof(Func<int, bool>),
                    typeof(GUIStyle)
                };


                prefixMethod = AccessTools.Method(typeof(EnumPopupInterceptor), "DoPopupPrefix", parameters);
                harmony.Patch(EditorGUIRef.doPopupMethod, new HarmonyMethod(prefixMethod));
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void Refresh()
        {
            if (Prefs.searchInEnumFields) Patch();
            else
            {
                harmony.Unpatch(EditorGUIRef.doPopupMethod, prefixMethod);
                harmony = null;
            }
        }
    }
}