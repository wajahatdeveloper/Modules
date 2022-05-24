#if UNITY_EDITOR
using System;
using UnityEditor;

	[Serializable]
	public class EditorPrefsBool : EditorPrefsType
	{
		public bool Value
		{
			get => EditorPrefs.GetBool(Key);
			set => EditorPrefs.SetBool(Key, value);
		}
		
		public EditorPrefsBool(string key) => Key = key;
		
		public static EditorPrefsBool WithKey(string key) => new EditorPrefsBool(key);
	}
#endif
