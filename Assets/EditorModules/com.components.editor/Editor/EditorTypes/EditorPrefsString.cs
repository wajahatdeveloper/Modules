#if UNITY_EDITOR
using System;
using UnityEditor;


	[Serializable]
	public class EditorPrefsString : EditorPrefsType
	{
		public string Value
		{
			get => EditorPrefs.GetString(Key);
			set => EditorPrefs.SetString(Key, value);
		}
		
		public EditorPrefsString(string key) => Key = key;
		
		public static EditorPrefsString WithKey(string key) => new EditorPrefsString(key);
	}

#endif
