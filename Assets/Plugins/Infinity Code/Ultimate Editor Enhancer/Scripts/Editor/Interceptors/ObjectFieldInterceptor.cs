/*           INFINITY CODE          */
/*     https://infinity-code.com    */

#if UNITY_2021_2_OR_NEWER
#define DECM2
#endif

using System;
using System.Reflection;
using HarmonyLib;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.Interceptors
{
    [InitializeOnLoad]
    public static class ObjectFieldInterceptor
    {
        private static Harmony harmony;
        private static MethodInfo originalMethod;
        private static MethodInfo prefixMethod;

        public delegate void GUIDelegate(Rect position,
            Rect dropRect,
            int id,
            UnityEngine.Object obj,
            UnityEngine.Object objBeingEdited,
            System.Type objType,
            System.Type additionalType,
            SerializedProperty property,
            object validator,
            bool allowSceneObjects,
            GUIStyle style);

        public static GUIDelegate OnGUIBefore;

        static ObjectFieldInterceptor()
        {
            if (Prefs.searchInEnumFields) Patch();
        }

        private static void DoObjectFieldPrefix(
            Rect position,
            Rect dropRect,
            int id,
            UnityEngine.Object obj,
            UnityEngine.Object objBeingEdited,
            System.Type objType,
#if DECM2
            System.Type additionalType,
#endif
            SerializedProperty property,
            object validator,
            bool allowSceneObjects,
            GUIStyle style)
        {
            if (OnGUIBefore != null)
            {
#if !DECM2
                System.Type additionalType = null;
#endif
                OnGUIBefore(position, dropRect, id, obj, objBeingEdited, objType, additionalType, property, validator, allowSceneObjects, style);
            }
        }

        private static void Patch()
        {
            try
            {
                harmony = new Harmony("InfinityCode.UltimateEditorEnhancer.ObjectFieldInterceptor");

                Type validatorType = typeof(EditorGUI).GetNestedType(
#if DECM2
                    "ObjectFieldValidatorOptions"
#else
                    "ObjectFieldValidator"
#endif
                    , BindingFlags.Public | BindingFlags.NonPublic);

                Type[] parameters = {
                    typeof(Rect),
                    typeof(Rect),
                    typeof(int),
                    typeof(UnityEngine.Object),
                    typeof(UnityEngine.Object),
                    typeof(Type),
#if DECM2
                    typeof(Type),
#endif
                    typeof(SerializedProperty),
                    validatorType,
                    typeof(bool),
                    typeof(GUIStyle)
                };

                MethodInfo[] methods = typeof(EditorGUI).GetMethods(Reflection.StaticLookup);
                foreach (MethodInfo info in methods)
                {
                    if (info.Name != "DoObjectField") continue;
                    ParameterInfo[] ps = info.GetParameters();
                    if (ps.Length != parameters.Length) continue;

                    originalMethod = info;
                    break;
                }

                prefixMethod = AccessTools.Method(typeof(ObjectFieldInterceptor), "DoObjectFieldPrefix", parameters);
                harmony.Patch(originalMethod, new HarmonyMethod(prefixMethod));
            }
            catch
            {
                //Debug.LogException(e);
            }
        }

        public static void Refresh()
        {
            if (Prefs.searchInEnumFields) Patch();
            else harmony.Unpatch(originalMethod, prefixMethod);
        }
    }
}