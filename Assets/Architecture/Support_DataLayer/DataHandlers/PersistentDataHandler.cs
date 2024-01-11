using System;
using UnityEngine;

namespace DataLayer
{
    [RunFirst]
    internal class PersistentDataHandler : MonoBehaviour
    {
        private const string LogClassName = "0_PersistentDataHandler";

        private static string dataFilePath = "";
        private static ES3Settings cacheSettings;

        private void OnEnable()
        {
            if (dataFilePath == "") { dataFilePath = Application.persistentDataPath + "/gameData.json"; }

#if UNITY_EDITOR
            //* dataFilePath =  Application.persistentDataPath + $"/gameData_{MultiPlay.Utils.GetCurrentCloneIndex()}.json";
#endif

            ES3.Init();
            cacheSettings = new ES3Settings(dataFilePath, ES3.Location.Cache)
            {
                bufferSize = 4096
            };

            ES3.Save("CacheInit", 1, cacheSettings);    // required to save file the first time

            LoadFile();
        }

        private static void LoadFile()
        {
            if (!ES3.FileExists(dataFilePath))
            {
                DebugX.Log($"{LogClassName} : Persistent Data File Does not exist.", LogFilters.None, null);
                return;
            }

            DebugX.Log($"{LogClassName} : Loading Persistent Data From File.", LogFilters.None, null);

            try
            {
                ES3.CacheFile(dataFilePath, cacheSettings);
            }
            catch (Exception e)
            {
                Debug.LogError($"PersistentDataHandler: Error => {e.Message}");
            }
        }

        private static void SaveFile()
        {
            DebugX.Log($"{LogClassName} : Saving Persistent Data To File.", LogFilters.None, null);
            ES3.StoreCachedFile(dataFilePath, cacheSettings);
        }

        private void OnApplicationFocus(bool pauseStatus)
        {
            DebugX.Log($"{LogClassName} : OnApplicationFocus , pauseStatus:{pauseStatus}", LogFilters.Events, gameObject);
            if (pauseStatus) { SaveFile(); }
        }

        private void OnApplicationQuit()
        {
            DebugX.Log($"{LogClassName} : OnApplicationQuit", LogFilters.Events, gameObject);
            SaveFile();
        }

        public static bool ContainsKey(string key)
        {
            return ES3.KeyExists(key, cacheSettings);
        }

        public static void SetData<T>(string key, T value)
        {
            ES3.Save<T>(key, value, cacheSettings);
        }

        public static T GetData<T>(string key, T defaultValue = default)
        {
            return ES3.Load<T>(key, defaultValue, cacheSettings);
        }
    }
}